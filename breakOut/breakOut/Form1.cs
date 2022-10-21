using Microsoft.VisualBasic.FileIO;

namespace breakOut
{
    public partial class Form1 : Form
    {
        //משתני המחלקה
        public bool goRight = false;
        public bool goLeft = false;
        public int ballX = 5;
        public int ballY = 5;
        public int score = 0;
        public int speed = 10;
        public int lifes = 3;
        public Form1()
        {
            InitializeComponent();
        }
        //בדיקת הפסד
        private void GameOver()
        {
            timer1.Stop();
        }
        //כאשר לוחצים על כפתור
        private void keyIsDown(object sender, KeyEventArgs e)
        {
            //במקרה וישנו לחיצה כפתור החץ השמאלי ואנו לא נמצאים בצד הכי שמאלי של המגרש תחזיר אמת 
            if (e.KeyCode == Keys.Left && player.Left > 0)
                goLeft = true;
            //במקרה וישנו לחיצה כפתור החץ השמאלי ואנו לא נמצאים בצד הכי שמאלי של המגרש תחזיר אמת 
            if (e.KeyCode == Keys.Right && player.Left + player.Width < 1028)
                goRight = true;

        }
        //כאשר עוזבים כפתור 
        private void keyIsUp(object sender, KeyEventArgs e)
        {
            //במקרה והכפתור לחוץ תחזיר שקר
            if (e.KeyCode == Keys.Left && player.Left > 0)
                goLeft = false;
            //במקרה והכפתור לחוץ תחזיר שקר
            if (e.KeyCode == Keys.Right)
                goRight = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //תזוזה של הכדור או ימינה או למטה על פי הזמן שעבר
            ball.Left += ballX;
            ball.Top += ballY;
            //עדכון הלייבל על פי כל פגיעה
            label1.Text = $"Score: {score}";
            //במקרה ואחד מהבוליאנים של התזוזה דולק השחקן זז
            if (goLeft)
            {
                player.Left -= speed;
            }
            if (goRight)
            {
                player.Left += speed;
            }
            //בדיקות כדי לעצור את השחקן מצלצאת מהמסך
            if (player.Left <= 0)
            {
                goLeft = false;
            }

            if (player.Left + player.Width >= 1025)
            {
                goRight = false;
            }
            //הולך לגרום לכדור לא לעבור את המגבלות של הפורום
            if (ball.Left + ball.Width > ClientSize.Width || ball.Left < 0)
            {
                ballX = -ballX;
            }
            
            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds))
            {
                ballY = -ballY; 
            }

            if (ball.Top + ball.Height > ClientSize.Height)
            {
                lifes--;
                label2.Text = $"life: {lifes}";
                ball.Left = 487;
                ball.Top = 350;
                ballX = 5;
                ballY = 5;
                if (lifes == 0)
                {
                    GameOver();
                    MessageBox.Show("Haha, you lose");

                    
                }
            }
            //רץ על כל האובייקטים של הבלוקים
            foreach (Control x in this.Controls)
            {

                if (x.Tag == "block")
                {
                    //במידה ויש פגיעה בין 2 ההיטבוקסים של הכדור והבלוקים
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        //מחיקת הבלוק ושינוי ערך הוואי לשלילי
                        Controls.Remove(x); 
                        ballY = -ballY;
                        score++;
                        ballX += 1;
                        ballY += 1;
                        if (score == 28)
                        {
                            GameOver();
                            MessageBox.Show("EZ WIN");
                        }
                    }
                }


            }
        }
    }
}
