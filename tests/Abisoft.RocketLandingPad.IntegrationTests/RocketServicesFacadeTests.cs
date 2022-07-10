using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.IntegrationTests;

[Trait("Category", "Integration")]
public class RocketServicesFacadeTests
{
    private readonly RocketServicesFacade _subject;

    public RocketServicesFacadeTests()
    {
        _subject = new();
    }

    [Fact]
    public void RocketServicesFacade_StartingAndLandingRocketsScenario()
    {
        /*
         *    0    1    2    3    4    5    6    7    8    9 
         * 0 [T]  [T]  [T]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 1 [T]  [T]  [T]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 2 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 3 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 4 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 5 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 6 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 7 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 8 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 9 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
        */
        // Arrange
        var area51 = _subject.AreaService.Create("Area 51", new(10, 10));

        var teslaPlatform = _subject.PlatformService.Create("Tesla platform", new(2, 3));
        var amazonPlatform = _subject.PlatformService.Create("Amazon platform", new(6, 2));
        var usaPlatform = _subject.PlatformService.Create("USA platform", new(4, 4));

        var elonsRocket = _subject.RocketService.Create("Elon Musk's rocket");
        var bezosRocket = _subject.RocketService.Create("Jeff Bezos' rocket");
        var bidensRocket = _subject.RocketService.Create("Joe Biden's rocket");

        var teslaPlatformAssigmentResult = _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = teslaPlatform,
            Position = new(0, 0),
        });

        var amazonPlatformAssigmentResult = _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = amazonPlatform,
            Position = new(4, 8),
        });

        var usaPlatformAssigmentResult = _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = usaPlatform,
            Position = new(6, 2),
        });

        // Act
        var elonsRocketLandingResult = _subject.LandingService.LandRocket(new()
        {
            Area = area51,
            Rocket = elonsRocket,
            Position = new(1, 1),
        });

        var bezosRocketLandingResult = _subject.LandingService.LandRocket(new()
        {
            Area = area51,
            Rocket = bezosRocket,
            Position = new(9, 9),
        });

        var bidensRocketLandingAttemptOne = _subject.LandingService.CanLandRocketInfo(new()
        {
            Area = area51,
            Rocket = bidensRocket,
            Position = new(2, 1),
        });

        var bidensRocketLandingAttemptTwo = _subject.LandingService.CanLandRocketInfo(new()
        {
            Area = area51,
            Rocket = bidensRocket,
            Position = new(1, 1),
        });

        var bidensRocketLandingAttemptThree = _subject.LandingService.CanLandRocketInfo(new()
        {
            Area = area51,
            Rocket = bidensRocket,
            Position = new(9, 9),
        });

        var bidensRocketLandingAttemptFour = _subject.LandingService.CanLandRocketInfo(new()
        {
            Area = area51,
            Rocket = bidensRocket,
            Position = new(0, 0),
        });

        var elonsRocketStartResult = _subject.LandingService.StartRocket(new()
        {
            Area = area51,
            Rocket = elonsRocket,
        });

        var bidensRocketLandingAttemptFive = _subject.LandingService.CanLandRocketInfo(new()
        {
            Area = area51,
            Rocket = bidensRocket,
            Position = new(15, 15),
        });

        var bidensRocketLandingAttemptSix = _subject.LandingService.CanLandRocketInfo(new()
        {
            Area = area51,
            Rocket = bidensRocket,
            Position = new(0, 0),
        });

        // Assert
        teslaPlatformAssigmentResult.Should().Be(new Result());
        amazonPlatformAssigmentResult.Should().Be(new Result());
        usaPlatformAssigmentResult.Should().Be(new Result());

        elonsRocketLandingResult.Should().Be(new Result());
        bezosRocketLandingResult.Should().Be(new Result());

        elonsRocketStartResult.Should().Be(new Result());

        bidensRocketLandingAttemptOne.Should().Be(Consts.LandingStates.OutOfPlatform);
        bidensRocketLandingAttemptTwo.Should().Be(Consts.LandingStates.Clash);
        bidensRocketLandingAttemptThree.Should().Be(Consts.LandingStates.Clash);
        bidensRocketLandingAttemptFour.Should().Be(Consts.LandingStates.Clash);
        bidensRocketLandingAttemptFive.Should().Be(Consts.LandingStates.OutOfPlatform);
        bidensRocketLandingAttemptSix.Should().Be(Consts.LandingStates.OkForLanding);
    }

    [Fact]
    public async Task RocketServicesFacade_LandingMultipleRocketsScenario()
    {
        /*
         *    0    1    2    3    4    5    6    7    8    9 
         * 0 [T]  [T]  [T]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 1 [T]  [T]  [T]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 2 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 3 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]
         *                                                   
         * 4 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 5 [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 6 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 7 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 8 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
         *                                                   
         * 9 [ ]  [B]  [B]  [B]  [B]  [ ]  [ ]  [ ]  [A]  [A]
        */
        // Arrange
        var area51 = _subject.AreaService.Create("Area 51", new(10, 10));

        var teslaPlatform = _subject.PlatformService.Create("Tesla platform", new(2, 3));
        var amazonPlatform = _subject.PlatformService.Create("Amazon platform", new(6, 2));
        var usaPlatform = _subject.PlatformService.Create("USA platform", new(4, 4));

        _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = teslaPlatform,
            Position = new(0, 0),
        });

        _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = amazonPlatform,
            Position = new(4, 8),
        });

        _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = usaPlatform,
            Position = new(6, 2),
        });

        const int rocketsCount = 10_000;

        var rockets = Enumerable.Range(1, rocketsCount)
            .Select(i => _subject.RocketService.Create($"Rocket {i}"))
            .ToList();

        var landingTasks = rockets.ConvertAll(r => Task.Run(() => _subject.LandingService.LandRocket(new()
        {
            Area = area51,
            Rocket = r,
            Position = new(7, 3),
        })));

        // Act
        var results = await Task.WhenAll(landingTasks);

        // Assert
        results.Count(lr => lr.IsSuccess).Should().Be(1);
        results.Count(lr => lr.IsError).Should().Be(rocketsCount - 1);
    }

    [Fact]
    public async Task RocketServicesFacade_LandingThreeRockets()
    {
        /*
         *       0       1       2       3       4  
         *                                          
         * 0   [   ]   [ T ]   [ T ]   [   ]   [   ]
         *                                          
         *                                          
         * 1   [ U ]   [   ]   [   ]   [   ]   [   ]
         *                                          
         *                                          
         * 2   [ U ]   [   ]   [   ]   [ A ]   [ A ]
         *                                          
         *                                          
         * 3   [ U ]   [   ]   [   ]   [ A ]   [ A ] 
        */

        /*
         *       0       1       2       3       4  
         *                                          
         * 0   [ X ]   [ X ]   [R3 ]   [ X ]   [   ]
         *                                          
         *                                          
         * 1   [R1 ]   [ X ]   [ X ]   [ X ]   [ X ]
         *                                          
         *                                          
         * 2   [ X ]   [ X ]   [   ]   [ X ]   [R4 ]
         *                                          
         *                                          
         * 3   [R2 ]   [ X ]   [   ]   [ X ]   [ X ] 
        */

        // Arrange
        var area51 = _subject.AreaService.Create("Area 51", new(4, 5));

        var teslaPlatform = _subject.PlatformService.Create("Tesla platform", new(1, 2));
        var usaPlatform = _subject.PlatformService.Create("USA platform", new(3, 1));
        var amazonPlatform = _subject.PlatformService.Create("Amazon platform", new(2, 2));

        _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = teslaPlatform,
            Position = new(0, 1),
        });

        _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = usaPlatform,
            Position = new(1, 0),
        });

        _subject.AreaService.AssignLandingPlatform(new()
        {
            Area = area51,
            Platform = amazonPlatform,
            Position = new(2, 3),
        });

        const int rocketsCount = 4;

        var rockets = Enumerable.Range(1, rocketsCount)
            .Select(i => _subject.RocketService.Create($"Rocket {i}"))
            .ToList();

        var landingTasks = new List<Task<Result>>
        {
            Task.Run(() => _subject.LandingService.LandRocket(new()
            {
                Area = area51,
                Rocket = rockets[0],
                Position = new(1, 0),
            })),

            Task.Run(() => _subject.LandingService.LandRocket(new()
            {
                Area = area51,
                Rocket = rockets[1],
                Position = new(3, 0),
            })),

            Task.Run(() => _subject.LandingService.LandRocket(new()
            {
                Area = area51,
                Rocket = rockets[2],
                Position = new(0, 2),
            })),

            Task.Run(() => _subject.LandingService.LandRocket(new()
            {
                Area = area51,
                Rocket = rockets[3],
                Position = new(2, 4),
            })),
        };

        var expectedOccupiedCoordinates = new Coordinates[]
        {
            new(0, 0),
            new(0, 1),
            new(0, 2),
            new(0, 3),

            new(1, 0),
            new(1, 1),
            new(1, 2),
            new(1, 3),
            new(1, 4),

            new(2, 0),
            new(2, 1),
            new(2, 3),
            new(2, 4),

            new(3, 0),
            new(3, 1),
            new(3, 3),
            new(3, 4),
        };

        // Act
        var results = await Task.WhenAll(landingTasks);

        // Assert
        results.Should().AllSatisfy(r =>
        {
            r.Should().Be(Result.Success);
        });

        area51.Value!.OccupiedCoordinates.Should()
            .BeEquivalentTo(expectedOccupiedCoordinates);
    }
}
