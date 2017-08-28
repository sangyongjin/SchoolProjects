//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Twoobjectsframe : Form
{  private const int formwidth = 1280;  
   private const int formheight = 700;  
   private const int ball_a_radius = 20;
    private const int penwidth = 3;
    private Pen pen3 = new Pen(Color.Red, penwidth);
    private const int horizontaladjustment = 8;  
   private double ball_a_distance_moved_per_refresh = 5;  
    private bool reset_colorvall = false;
    private bool beginning = false;
    private double ball_a_real_coord_x = 640;
   private double ball_a_real_coord_y = 380;
    private double right = 1180;
    private double down = 500;
    private double left = 82;
    private double up = 40;
    private Button gobutton = new Button();
    private Button resetbutton = new Button();
    private Button exitbutton = new Button();
    private Button peusbutton = new Button();
    private TextBox numberspeed = new TextBox();
    private TextBox boxX = new TextBox();
    private TextBox boxY = new TextBox();
    private Label X = new Label();
    private Label Y = new Label();
    private Label Back = new Label();
    private Label title = new Label();
    private Label pixel = new Label();
    private int ball_a_int_coord_x;  //The x-coordinate of ball a.
   private int ball_a_int_coord_y;  //The y-coordinate of ball a.
   private double ball_a_horizontal_delta;
   private double ball_a_vertical_delta;
   private double ball_a_angle_radians;
   private const double graphicrefreshrate = 30.0;  
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double ball_a_update_rate = 30.0;  
   private static System.Timers.Timer ball_a_control_clock = new System.Timers.Timer();
   private bool ball_a_clock_active = false;  
   private Point location_of_go_button = new Point(450, 570);
   private Point location_of_reset_button = new Point(150, 570);
   private Point location_of_exit_button = new Point(750, 570);
    private Point location_of_text_speedbox = new Point(700, 600);
    private Point location_of_text_boxX = new Point(150, 600);
    private Point location_of_text_direction = new Point(800, 600);
    private Point location_of_direct = new Point(800, 560);
    private Point location_of_X = new Point(80, 600);
    private Point location_of_pixel = new Point(600, 600);
    private Point location_of_refreshpix = new Point(500, 560);
    private Point location_of_Y = new Point(930, 600);
    private Point location_of_text_boxY = new Point(1000, 600);
    private Point location_of_title = new Point(formwidth / 2 - formwidth / 4, 11);
    private int _x1;
    private int _y1;
    public Twoobjectsframe()   //The constructor of this class
   {  //Set the title of this form.
        DoubleBuffered = true;
      Text = "one Animated Balls by SangYong Jin";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
      //Set the initial size of this form
      Size = new Size(formwidth,formheight);
      //Set the background color of this form
      BackColor = Color.Green;
      //Set the initial coordinates of ball a.
      ball_a_int_coord_x = (int)(ball_a_real_coord_x);
      ball_a_int_coord_y = (int)(ball_a_real_coord_y);
      System.Console.WriteLine("Initial coordinates: ball_a_int_coord_x = {0}. ball_a_int_coord_y = {1}.",
                               ball_a_int_coord_x,ball_a_int_coord_y);
        //Set the initial coordinates of ball b.
      this.title.TextAlign = ContentAlignment.MiddleCenter;
      this.title.Font = new Font("Arial", 12, FontStyle.Regular);
      title.Text = "Animation by SangYongJin";
      title.Size = new Size(formwidth / 2, 20);
      title.Location = location_of_title;
      title.BackColor = Color.LightCyan;
      Controls.Add(title);
        pixel.Text = " Speed(pixel/second ";
        pixel.Size = new Size(100, 30);
        pixel.Location = location_of_pixel;
        Controls.Add(pixel);
       

        X.Text = " Player Left ";
      X.Size = new Size(70, 18);
      X.Location = location_of_X;
      Controls.Add(X);
      Y.Text = " Player Right ";
      Y.Size = new Size(70, 18);
      Y.Location = location_of_Y;
      Controls.Add(Y);



        boxX.Size = new Size(82, 25);
        boxX.Location = location_of_text_boxX;
        boxX.BackColor = Color.White;
        Controls.Add(boxX);

        boxY.Size = new Size(82, 25);
        boxY.Location = location_of_text_boxY;
        boxY.BackColor = Color.White;
        Controls.Add(boxY);
       

        numberspeed.Size = new Size(82, 25);
        numberspeed.Location = location_of_text_speedbox;
        numberspeed.BackColor = Color.White;
        Controls.Add(numberspeed);

        peusbutton.Text = "peuse";
        peusbutton.Size = new Size(82, 25);
        peusbutton.Location = location_of_go_button;
        peusbutton.BackColor = Color.Red;
        
        
        gobutton.Text = "Start";
        gobutton.Size = new Size(82, 25);
        gobutton.Location = location_of_go_button;
        gobutton.BackColor = Color.Red;
        Controls.Add(gobutton);
        resetbutton.Text = "New";
        resetbutton.Size = new Size(82, 25);
        resetbutton.Location = location_of_reset_button;
        resetbutton.BackColor = Color.White;
        Controls.Add(resetbutton);
        exitbutton.Text = "Exit";
        exitbutton.Size = new Size(82, 25);
        exitbutton.Location = location_of_exit_button;
        exitbutton.BackColor = Color.White;
        Controls.Add(exitbutton);
        


        Twoanimatedlogic algorithms = new Twoanimatedlogic();
      
      
      ball_a_angle_radians = algorithms.get_random_direction_for_a();
      System.Console.WriteLine("Direction of ball a = {0} degrees", ball_a_angle_radians * 90.0 / System.Math.PI);
     
      

       ball_a_horizontal_delta = ball_a_distance_moved_per_refresh*System.Math.Cos(ball_a_angle_radians);
      ball_a_vertical_delta = ball_a_distance_moved_per_refresh*System.Math.Sin(ball_a_angle_radians);

      
    
      

      graphic_area_refresh_clock.Enabled = false;  
      graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Updatedisplay);  

      
      ball_a_control_clock.Enabled = false; 
      ball_a_control_clock.Elapsed += new ElapsedEventHandler(Updateballa);

     

      Startgraphicclock(graphicrefreshrate);  
      Startballaclock(ball_a_update_rate);    


      exitbutton.Click += new EventHandler(exitfromthisprogram);
        resetbutton.Click += new EventHandler(reset);
        gobutton.Click += new EventHandler(goobutton);
        _x1 = 100;
        _y1 = 200;

    }//End of constructor

    private void FormView_KeyDown(object sender, KeyEventArgs e)
    {

        if (e.KeyCode == Keys.Up)
        {
            if (_y1 - 20 > up)
            {
                _y1 -= 20;
                Rectangle rect = new Rectangle(_x1, _y1, 45, 4 * ball_a_radius);
                Invalidate();
            }

        }
        else if (e.KeyCode == Keys.Down)
        {
            if (_y1 + 100 < down)
            {
                _y1 += 20;
                Rectangle rect = new Rectangle(_x1, _y1, 45, 4 * ball_a_radius);
                Invalidate();
            }
        }
    }
   protected override void OnPaint(PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
       graph.FillRectangle(Brushes.LightGoldenrodYellow, 0, formheight - 140, formwidth, 202);
       graph.FillRectangle(Brushes.LightCyan, 0, 0, formwidth, 30);

  
        graph.DrawRectangle(pen3, 100, 60, 1100, 450);
        graph.FillRectangle(Brushes.Blue, _x1, _y1, 45, 4 * ball_a_radius);
 
        
            graph.FillEllipse(Brushes.Blue, ball_a_int_coord_x, ball_a_int_coord_y, 2 * ball_a_radius, 2 * ball_a_radius);
        
        
        if(reset_colorvall)
        {
            graph.FillEllipse(Brushes.Blue, ball_a_int_coord_x, ball_a_int_coord_y, 2 * ball_a_radius, 2 * ball_a_radius);
        }
       
        base.OnPaint(ee);
   }


    protected void reset(Object sender, EventArgs events)
    {

           ball_a_real_coord_x = 640;
           ball_a_real_coord_y = 380;

        ball_a_int_coord_x = (int)System.Math.Round(ball_a_real_coord_x);
         ball_a_int_coord_y = (int)System.Math.Round(ball_a_real_coord_y);

   
     
        numberspeed.Text = " ";
        ball_a_control_clock.Enabled = false;
        ball_a_clock_active = false;
        beginning = false;
    
        Invalidate();
    }

    protected void Startgraphicclock(double refreshrate)
    {
        double elapsedtimebetweentics;
        if (refreshrate < 2.0) refreshrate = 1.0;  //Avoid dividing by a number close to zero.
        elapsedtimebetweentics = 1000.0 / refreshrate;  //elapsedtimebetweentics has units milliseconds.
        graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsedtimebetweentics);
        graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
    }

    protected void Startballaclock(double updaterate)
    {
        double elapsedtimebetweenballmoves;
        double a = 45;
      //  Double.TryParse(direction.out a);
        ball_a_horizontal_delta = ball_a_distance_moved_per_refresh * System.Math.Cos(a * System.Math.PI / 180.0);
        ball_a_vertical_delta = ball_a_distance_moved_per_refresh * System.Math.Sin(a * System.Math.PI / 180.0);
        if (updaterate < 1.0) updaterate = 1.0;  //This program does not allow updates slower than 1 Hz.
        double i = 1;
        if (string.IsNullOrWhiteSpace(numberspeed.Text))
        {
            i = updaterate;
        }
        else
        {
            Double.TryParse(numberspeed.Text, out i);
        }

        elapsedtimebetweenballmoves = 1000.0 / i;  //1000.0ms = 1second.  elapsedtimebetweenballmoves has units milliseconds.
        ball_a_control_clock.Interval = (int)System.Math.Round(elapsedtimebetweenballmoves);
        ball_a_control_clock.Enabled = false;   //Start clock ticking.
        ball_a_clock_active = true;
}
  

    protected void Updatedisplay(System.Object sender, ElapsedEventArgs evt)
   {  Invalidate();  
      if(!(ball_a_clock_active ))
          {graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }

   protected void Updateballa(System.Object sender, ElapsedEventArgs evt)
   {
        if (beginning == true)
        {

            ball_a_real_coord_x = ball_a_real_coord_x + ball_a_horizontal_delta;
            ball_a_real_coord_y = ball_a_real_coord_y - ball_a_vertical_delta;
        }
      ball_a_int_coord_x = (int)System.Math.Round(ball_a_real_coord_x);
      ball_a_int_coord_y = (int)System.Math.Round(ball_a_real_coord_y);
        

        if (ball_a_real_coord_y <= up)
        {

            ball_a_vertical_delta = ball_a_distance_moved_per_refresh * -1;



        }
        if (ball_a_int_coord_x >= right)
        {

            ball_a_horizontal_delta = ball_a_distance_moved_per_refresh * -1;
        }
        if (ball_a_int_coord_x <=left)
        {

            ball_a_horizontal_delta = ball_a_distance_moved_per_refresh * 1;
        }

        if (ball_a_real_coord_y >= down)
        {

            ball_a_vertical_delta = ball_a_distance_moved_per_refresh * 1;



        }

    }//End of method Updateballa

    protected void exitfromthisprogram(Object sender, EventArgs events)
   {
       System.Console.WriteLine("This program will end execution.");
       Close();
   }

  
    void Timer(Object sender, EventArgs events)
   {
        ball_a_real_coord_x = System.Math.Round(ball_a_real_coord_x);
        ball_a_real_coord_y = System.Math.Round(ball_a_real_coord_y);
        boxX.Text = ball_a_real_coord_x.ToString();
       boxY.Text = ball_a_real_coord_y.ToString();

     
        
    }
   protected void goobutton(Object sender, EventArgs events)
   {
        
        var timer = new System.Windows.Forms.Timer();
       timer.Tick += new EventHandler(Timer);
       timer.Interval = 100;
       timer.Start();
        beginning = true;
       Startballaclock(ball_a_update_rate);
        Startgraphicclock(graphicrefreshrate);
        ball_a_clock_active = true;
       
        ball_a_control_clock.Enabled = true;//Start clock ticking.
       // graphic_area_refresh_clock.Enabled = true;
       // ball_a_clock_active = true;


        Invalidate();
       System.Console.WriteLine("You clicked on the Draw button.");
   }

   private void InitializeComponent()
   {
 

            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.KeyPreview = true;
            this.Name = "ballframe";
            this.Load += new System.EventHandler(this.Twoobjectsframe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.ResumeLayout(false);

   }

   private void Twoobjectsframe_Load(object sender, EventArgs e)
   {

   }


}



