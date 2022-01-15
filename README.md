# What Is It?

`PlayPlayMini.UI` is an extension for `PlayPlayMini`, adding a skinnable, object-oriented UI framework.

# Documentation; How to Use `PlayPlayMini.UI`

This documentation assumes you already have a project that uses the `PlayPlayMini` framework. If not, check out the `PlayPlayMini` framework documentation to get started with it!

## Load Required Resources

`PlayPlayMini.UI` requires a couple spritesheets, an image, and a font to be loaded. These are used for rendering buttons, checkbox, and the mouse cursor.

```C#
var gsmBuilder = new GameStateManagerBuilder()
	.AddPictures(new PictureMeta[] {
		...
		new PictureMeta("Cursor", "Graphics/Cursor"),
	})
	.AddSpriteSheets(new SpriteSheetMeta[] {
		...
		new SpriteSheetMeta("Button", "Graphics/Button", 4, 16),
		new SpriteSheetMeta("Checkbox", "Graphics/Checkbox", 10, 8),
	})
	.AddFonts(new FontMeta[] {
		new FontMeta("Font", "Graphics/Font", 6, 8),
	})
	...
;
```

At the time of this writing, `PlayPlayMini.UI` is not as flexible as it could be with the size of the UI elements it renders. For the best effect, use a font close to 8 pixels in height.

## Configure UI Framework

In the `PlayPlayMini`, it is best-practice to have a Startup GameState which displays a loading message to the player while your deferred resources load.

One of the primary reasons for using `PlayPlayMini.UI` is for a mouse-driven UI. If you're not already doing so, you should configure the MouseManager in your Startup GameState. For example:

```C#
class Startup : IGameState
{
	private MouseManager Mouse { get; }

	public Startup(MouseManager mouse, ...)
	{
		Mouse = mouse;
		...

		Mouse.PictureName = "Cursor";
		Mouse.Hotspot = (3, 1);
	}
}
```



## In a GameState, Request an Instance of UIService, and Configure It

Each instance of `UIService` that's requested is a new instance. A `UIService` instance contains UI components, such as buttons, and is responsible for handling mouse events that take place within it, including hovers & clicks, and notifying the appropriate component(s) of these events.

Example:

```C#
class PauseMenu: IGameState
{
	private UIService UI { get; }
	...

	public PauseMenu(UIService ui, ...)
	{
		UI = ui;
		...

		InitUI();
	}

	// it's not required to create a dedicated method for building the UI
	// (you'll only ever call it once), but it helps keep the constructor tidy.
	private void InitUI()
	{
		// Window is a UI component provided by PlayPlayMini.UI
		var window = new Window(
			UI,			// instance of the UIService
			20, 20,		// x, y coordinate to position the window
			150, 100,	// width, height of the window
			"Paused"	// title
		);

		var resume = new Button(UI, 4, 16, "Resume", ResumeGameHandler);
		var settings = new Button(UI, 4, 33, "Settings", SettingsHandler);
		var saveAndQuit = new Button(UI, 4, 50, "Save & Quit", SaveAndQuitHandler);

		// add the buttons to the window:
		window.AddUIElements(resume, settings, saveAndQuit);

		// and the window to the UI canvas:
		UI.Canvas.AddUIElements(window);
	}

	private void ResumeGameHandler(int x, int y)
    {
		// x, y coordinates that click took place, relative to the button's position
		// do something
	}

	private void SettingsHandler(int x, int y)
    {
		// do something
	}

	private void SaveAndQuitHandler(int x, int y)
    {
		// do something
	}

	public void ActiveUpdate(GameTime gameTime)
    {
		UI.ActiveUpdate(gameTime);
    }

	public void AlwaysDraw(GameTime gameTime)
	{
		UI.AlwaysDraw(gameTime);
	}

	public void ActiveDraw(GameTime gameTime)
    {
		UI.ActiveDraw(gameTime);
		// note: there is no need to manually update or draw the mouse cursor;
		// if you're using a UIService, it will handle the mouse for you.
    }
}
```

