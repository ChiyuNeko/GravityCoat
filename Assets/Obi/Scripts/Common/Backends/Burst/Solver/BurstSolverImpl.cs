#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)

namespace Obi

        // simplices:
        public NativeArray<int> simplices;

        // cached particle data arrays (just wrappers over raw unmanaged data held by the abstract solver)
        public NativeArray<int> activeParticles;
        public NativeArray<float4x4> anisotropies;

        // aux foam data:
        public NativeArray<float4> auxPositions;
        public NativeArray<float4> auxVelocities;
        public NativeArray<float4> auxColors;
        public NativeArray<float4> auxAttributes;

        public NativeArray<BurstAabb> reducedBounds;

            // Initialize contact generation acceleration structure:
            particleGrid = new ParticleGrid();

            cellCoords = abstraction.cellCoords.AsNativeArray<int4>();

            if (reducedBounds.IsCreated)

            reducedBounds = new NativeArray<BurstAabb>(counts.simplexCount, Allocator.Persistent);
            BurstJobHandle burstHandle = inputDeps as BurstJobHandle;

            // calculate bounding boxes for all simplices:
            var boundsJob = new CalculateSimplexBoundsJob()
            {
                radii = principalRadii,
                fluidMaterials = fluidMaterials,
                positions = positions,
                velocities = velocities,
                simplices = simplices,
                simplexCounts = simplexCounts,
                particleMaterialIndices = collisionMaterials,
                collisionMaterials = ObiColliderWorld.GetInstance().collisionMaterials.AsNativeArray<BurstCollisionMaterial>(),
                parameters = abstraction.parameters,
                simplexBounds = simplexBounds,
                reducedBounds = reducedBounds,
                dt = stepTime
            };

            burstHandle.jobHandle = boundsJob.Schedule(simplexCounts.simplexCount, 64, burstHandle.jobHandle);

            // parallel reduction:
            int chunkSize = 4;
        {
            // update solver bounds struct:
            if (reducedBounds.IsCreated && reducedBounds.Length > 0)
            {
                solverBounds.min = reducedBounds[0].min;
                solverBounds.max = reducedBounds[0].max;
            }

            min = solverBounds.min.xyz;
            max = solverBounds.max.xyz;
        }

        {
            if (auxPositions.IsCreated)
                auxPositions.Dispose();
            if (auxVelocities.IsCreated)
                auxVelocities.Dispose();
            if (auxColors.IsCreated)
                auxColors.Dispose();
            if (auxAttributes.IsCreated)
                auxAttributes.Dispose();

            auxPositions = new NativeArray<float4>((int)abstraction.maxFoamParticles, Allocator.Persistent);
            auxVelocities = new NativeArray<float4>((int)abstraction.maxFoamParticles, Allocator.Persistent);
            auxColors = new NativeArray<float4>((int)abstraction.maxFoamParticles, Allocator.Persistent);
            auxAttributes = new NativeArray<float4>((int)abstraction.maxFoamParticles, Allocator.Persistent);
        }
            // Wipe all forces to zero. However we can't wipe wind here, since we
            // need wind values during interpolation to calculate rope normals.
            abstraction.externalForces.WipeToZero();
            abstraction.externalTorques.WipeToZero();

            abstraction.externalForces.Upload();
            abstraction.externalTorques.Upload();

            // store current end positions as the start positions for the next step.
            abstraction.startPositions.CopyFrom(abstraction.endPositions);
            abstraction.startOrientations.CopyFrom(abstraction.endOrientations);
            abstraction.endPositions.CopyFrom(abstraction.positions);

        public void PushData()
        {
            // Initialize wind values with solver's ambient wind.
            abstraction.wind.WipeToValue(abstraction.parameters.ambientWind);
            abstraction.wind.Upload();
        }

        public void RequestReadback()
        {
        }

            burstHandle.jobHandle = FindFluidParticles(burstHandle.jobHandle);
            if (particleBatchData.IsCreated)

            // get constraint parameters for constraint types that depend on broadphases:
            var collisionParameters = abstraction.GetConstraintParameters(Oni.ConstraintType.Collision);
                // generate particle-particle and particle-collider interactions in parallel:
                JobHandle generateParticleInteractionsHandle = inputDeps, generateContactsHandle = inputDeps;

                // allocate arrays for interactions and batch data:

                // allocate effective mass arrays:
                abstraction.contactEffectiveMasses.ResizeUninitialized(colliderGrid.colliderContactQueue.Count);
                abstraction.particleContactEffectiveMasses.ResizeUninitialized(particleGrid.particleContactQueue.Count);

                // dequeue contacts/interactions into temporary arrays:
                var rawParticleContacts = new NativeArray<BurstContact>(particleGrid.particleContactQueue.Count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

                abstraction.particleContacts.ResizeUninitialized(particleGrid.particleContactQueue.Count);

                abstraction.colliderContacts.ResizeUninitialized(colliderGrid.colliderContactQueue.Count);

            // Apply aerodynamics
            burstHandle.jobHandle = constraints[(int)Oni.ConstraintType.Aerodynamics].Project(burstHandle.jobHandle, stepTime, substepTime, steps, timeLeft);

            // Predict positions:
            var predictPositions = new PredictPositionsJob()

            // Project position constraints:
            burstHandle.jobHandle = ApplyConstraints(burstHandle.jobHandle, stepTime, substepTime, steps, timeLeft);

            // velocity constraints:
            burstHandle.jobHandle = ApplyVelocityCorrections(burstHandle.jobHandle, substepTime);

            // Update diffuse particles:
            int substepsLeft = (int)math.round(timeLeft / substepTime); 
            int foamPadding = (int)math.ceil(abstraction.substeps / (float)abstraction.foamSubsteps);

            if (substepsLeft % foamPadding == 0)
                burstHandle.jobHandle = UpdateDiffuseParticles(burstHandle.jobHandle, substepTime * foamPadding);

                blendFactor = stepTime > 0 ? unsimulatedTime / stepTime : 0,

            // Update deformable edge normals
            var updateEdgeNormals = new UpdateEdgeNormalsJob()

            // Update deformable triangle orientations
            var updateOrientations = new RenderableOrientationFromNormals()

            //make sure density constraints are enabled, otherwise particles have no neighbors and neighbor lists will be uninitialized.
            var param = abstraction.GetConstraintParameters(Oni.ConstraintType.Density);
            {
                // Fluid laplacian/anisotropy (only if we're in play mode, in-editor we have no particlegrid/sorted data).
                var d = constraints[(int)Oni.ConstraintType.Density] as BurstDensityConstraints;
                if (Application.isPlaying && d != null)
                    burstHandle.jobHandle = d.CalculateAnisotropyLaplacianSmoothing(burstHandle.jobHandle);
        {
            var system = abstraction.GetRenderSystem<ObiFoamGenerator>() as BurstFoamRenderSystem;
            if (system != null)
            {
                int* dispatchPtr = (int*)abstraction.foamCount.AddressOfElement(0);

                for (int i = 0; i < system.renderers.Count; ++i)
                {
                    var emitJob = new EmitParticlesJob
                    {
                        // when the actor gets removed from solver, solverIndices is destroyed and
                        // this job may still be running. As a solution, create a temporary copy of the array.
                        activeParticles = new NativeArray<int>(system.renderers[i].actor.solverIndices.AsNativeArray<int>(), Allocator.TempJob), 
                        positions = prevPositions,
                        velocities = velocities,
                        angularVelocities = angularVelocities,
                        principalRadii = principalRadii,

                        outputPositions = abstraction.foamPositions.AsNativeArray<float4>(),
                        outputVelocities = abstraction.foamVelocities.AsNativeArray<float4>(),
                        outputColors = abstraction.foamColors.AsNativeArray<float4>(),
                        outputAttributes = abstraction.foamAttributes.AsNativeArray<float4>(),

                        dispatchBuffer = abstraction.foamCount.AsNativeArray<int>(),

                        vorticityRange = system.renderers[i].vorticityRange,
                        velocityRange = system.renderers[i].velocityRange,
                        foamGenerationRate = system.renderers[i].foamGenerationRate,
                        potentialIncrease = system.renderers[i].foamPotential,
                        potentialDiffusion = math.pow(1 - math.saturate(system.renderers[i].foamPotentialDiffusion), deltaTime),
                        buoyancy = system.renderers[i].buoyancy,
                        drag = system.renderers[i].drag,
                        airdrag = math.pow(1 - math.saturate(system.renderers[i].atmosphericDrag), deltaTime),
                        isosurface = system.renderers[i].isosurface,
                        airAging = system.renderers[i].airAging,
                        particleSize = system.renderers[i].size,
                        sizeRandom = system.renderers[i].sizeRandom,
                        lifetime = system.renderers[i].lifetime,
                        lifetimeRandom = system.renderers[i].lifetimeRandom,
                        foamColor = (Vector4)system.renderers[i].color,

                        deltaTime = deltaTime
                    };

                    inputDeps = emitJob.Schedule(system.renderers[i].actor.activeParticleCount, 128, inputDeps);
                }

                var updateJob = new UpdateParticlesJob
                {
                    positions = prevPositions,
                    orientations = renderableOrientations,
                    principalRadii = renderableRadii,
                    velocities = velocities,
                    fluidData = fluidData,
                    fluidMaterial = fluidMaterials,

                    simplices = simplices,
                    simplexCounts = simplexCounts,

                    grid = particleGrid.grid,
                    gridLevels = particleGrid.grid.populatedLevels.GetKeyArray(Allocator.TempJob),

                    densityKernel = new Poly6Kernel(abstraction.parameters.mode == Oni.SolverParameters.Mode.Mode2D),

                    inputPositions = abstraction.foamPositions.AsNativeArray<float4>(),
                    inputVelocities = abstraction.foamVelocities.AsNativeArray<float4>(),
                    inputColors = abstraction.foamColors.AsNativeArray<float4>(),
                    inputAttributes = abstraction.foamAttributes.AsNativeArray<float4>(),

                    outputPositions = auxPositions,
                    outputVelocities = auxVelocities,
                    outputColors = auxColors,
                    outputAttributes = auxAttributes,

                    dispatchBuffer = abstraction.foamCount.AsNativeArray<int>(),

                    parameters = abstraction.parameters,

                    agingOverPopulation = new Vector3(abstraction.foamAccelAgingRange.x, abstraction.foamAccelAgingRange.y, abstraction.foamAccelAging),
                    currentAliveParticles = dispatchPtr[3],
                    deltaTime = deltaTime
                };

                inputDeps = IJobParallelForDeferExtensions.Schedule(updateJob, &dispatchPtr[3], 64, inputDeps);

                var copyJob = new CopyJob
                {
                    inputPositions = auxPositions,
                    inputVelocities = auxVelocities,
                    inputColors = auxColors,
                    inputAttributes = auxAttributes,

                    outputPositions = abstraction.foamPositions.AsNativeArray<float4>(),
                    outputVelocities = abstraction.foamVelocities.AsNativeArray<float4>(),
                    outputColors = abstraction.foamColors.AsNativeArray<float4>(),
                    outputAttributes = abstraction.foamAttributes.AsNativeArray<float4>(),

                    dispatchBuffer = abstraction.foamCount.AsNativeArray<int>()
                };

                inputDeps = IJobParallelForDeferExtensions.Schedule(copyJob, &dispatchPtr[7], 256, inputDeps);

                activeFoamParticleCount = (uint)dispatchPtr[3];
            }
            return inputDeps;
        }
#endif

