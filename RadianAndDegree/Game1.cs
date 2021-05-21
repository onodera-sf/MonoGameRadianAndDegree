using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace RadianAndDegree
{
	/// <summary>
	/// ゲームメインクラス
	/// </summary>
	public class Game1 : Game
	{
    /// <summary>
    /// グラフィックデバイス管理クラス
    /// </summary>
    private readonly GraphicsDeviceManager _graphics = null;

    /// <summary>
    /// スプライトのバッチ化クラス
    /// </summary>
    private SpriteBatch _spriteBatch = null;

    /// <summary>
    /// スプライトでテキストを描画するためのフォント
    /// </summary>
    private SpriteFont _font = null;

    /// <summary>
    /// Degree の値
    /// </summary>
    private float _degree = 0.0f;

    /// <summary>
    /// 矢印テクスチャー
    /// </summary>
    private Texture2D _arrowTexture = null;

    /// <summary>
    /// テクスチャーの中心座標
    /// </summary>
    private Vector2 _textureCenter = Vector2.Zero;


    /// <summary>
    /// GameMain コンストラクタ
    /// </summary>
    public Game1()
    {
      // グラフィックデバイス管理クラスの作成
      _graphics = new GraphicsDeviceManager(this);

      // ゲームコンテンツのルートディレクトリを設定
      Content.RootDirectory = "Content";

      // マウスカーソルを表示
      IsMouseVisible = true;
    }

    /// <summary>
    /// ゲームが始まる前の初期化処理を行うメソッド
    /// グラフィック以外のデータの読み込み、コンポーネントの初期化を行う
    /// </summary>
    protected override void Initialize()
    {
      // TODO: ここに初期化ロジックを書いてください

      // コンポーネントの初期化などを行います
      base.Initialize();
    }

    /// <summary>
    /// ゲームが始まるときに一回だけ呼ばれ
    /// すべてのゲームコンテンツを読み込みます
    /// </summary>
    protected override void LoadContent()
    {
      // テクスチャーを描画するためのスプライトバッチクラスを作成します
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // フォントをコンテントパイプラインから読み込む
      _font = Content.Load<SpriteFont>("Font");

      // テクスチャーを読み込む
      _arrowTexture = Content.Load<Texture2D>("Arrow");

      // テクスチャーの中心座標
      _textureCenter = new Vector2(_arrowTexture.Width / 2,
                                       _arrowTexture.Height / 2);
    }

    /// <summary>
    /// ゲームが終了するときに一回だけ呼ばれ
    /// すべてのゲームコンテンツをアンロードします
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: ContentManager で管理されていないコンテンツを
      //       ここでアンロードしてください
    }

    /// <summary>
    /// 描画以外のデータ更新等の処理を行うメソッド
    /// 主に入力処理、衝突判定などの物理計算、オーディオの再生など
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Update(GameTime gameTime)
    {
      KeyboardState keyboardState = Keyboard.GetState();
      MouseState mouseState = Mouse.GetState();
      GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

      // ゲームパッドの Back ボタン、またはキーボードの Esc キーを押したときにゲームを終了させます
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      // キーボードによる回転
      if (keyboardState.IsKeyDown(Keys.Left))
      {
        _degree -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5.0f;
      }
      if (keyboardState.IsKeyDown(Keys.Right))
      {
        _degree += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5.0f;
      }

      // ゲームパッドによる回転
      if (gamePadState.IsConnected)
      {
        _degree += gamePadState.ThumbSticks.Left.X *
                        (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5.0f;
      }

      // マウスによる回転
      if (mouseState.LeftButton == ButtonState.Pressed)
      {
        _degree += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 5.0f;
      }

      // 登録された GameComponent を更新する
      base.Update(gameTime);
    }

    /// <summary>
    /// 描画処理を行うメソッド
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Draw(GameTime gameTime)
    {
      // 画面を指定した色でクリアします
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // スプライトの描画準備
      _spriteBatch.Begin();

      // 基本テクスチャー
      _spriteBatch.Draw(_arrowTexture, new Vector2(150.0f, 150.0f), Color.Yellow);

      // 回転テクスチャー
      _spriteBatch.Draw(_arrowTexture,
          new Vector2(150.0f, 150.0f) + _textureCenter,
          null, Color.Red, MathHelper.ToRadians(_degree), _textureCenter,
          1.0f, SpriteEffects.None, 0.0f);

      // Degree と Radian
      _spriteBatch.DrawString(_font,
          "Degree : " + _degree.ToString() + Environment.NewLine +
          "Radian : " + MathHelper.ToRadians(_degree).ToString(),
          new Vector2(30.0f, 30.0f), Color.White);

      // スプライトの一括描画
      _spriteBatch.End();

      // 登録された DrawableGameComponent を描画する
      base.Draw(gameTime);
    }
  }
}
