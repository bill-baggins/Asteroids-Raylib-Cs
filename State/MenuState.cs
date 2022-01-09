
namespace Asteroids.State
{
    public enum MenuState
    {
        MAIN_MENU = 0xfffad,
        OPTION_MENU,
        QUIT,
        GAME
    }

    public static class MenuStateManager
    {
        public static MenuState MenuState { get; set; } = MenuState.GAME;
    }
}
