using Architecture;
using System;
using System.Collections.Generic;

public class SceneConfigTest : SceneConfig
{
    public const string SCENE_NAME = "Architecture";

    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();
        this.CreateRepository<BankRepository>(repositoriesMap);
        return repositoriesMap;
    }

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();
        this.CreateInteractor<BankInteractor>(interactorsMap);
        return interactorsMap;
    }
}
