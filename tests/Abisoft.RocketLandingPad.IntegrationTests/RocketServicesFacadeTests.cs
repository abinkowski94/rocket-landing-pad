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
}
