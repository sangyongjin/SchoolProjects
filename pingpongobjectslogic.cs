//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**



public class Twoanimatedlogic
{   private System.Random randomgenerator = new System.Random();

    public double get_random_direction_for_a()
       {
        double randomnumber = -90;
        randomnumber = randomnumber - 0.5;
       



        
        double ball_a_angle_radians = System.Math.PI * randomnumber;
        
        return ball_a_angle_radians;
       }

   

}
