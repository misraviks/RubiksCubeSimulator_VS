using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RubiksCubeSimulator.Rubiks;

namespace RubiksCubeSimulator.Forms
{
    public partial class MainForm : Form
    {
        private RubiksCube rubiksCube;

        /// <summary>
        /// Gets all of the cube displays on the form.
        /// </summary>
        private IEnumerable<CubeFaceDisplay> CubeDisplays => 
            tableLayoutPanel.Controls.OfType<CubeFaceDisplay>();

        public MainForm()
        {
            InitializeComponent();
            ResizeRedraw = true;

            checkBoxLockColors.DataBindings.Add("Checked", 
                Settings.Instance, nameof(Settings.Instance.ColorsLocked));

            colorStrip.Colors = Settings.Instance.Palette;
            SetRubiksCube();
            SetHoverEffect();
            UpdateErrorStatus();
        }

        private void SetRubiksCube()
        {
            // Restore cube from settings
             rubiksCube = new RubiksCube(Settings.Instance.CubeColors);

            // Create a solved cube with the developers color scheme
            //rubiksCube = RubiksCube.Create(CubeColorScheme.DevsScheme);

            rubiksCube.MoveMade += RubiksCubeMoveMade;
            UpdateDisplayedCube();
        }

        private void UpdateErrorStatus()
        {
            var defects = rubiksCube.GetColorDefects();
            labellErrorStatus.Text = defects.GetShortReport();
        }

        private void SetHoverEffect()
        {
            foreach (var display in CubeDisplays)
            {
                if (checkBoxLockColors.Checked)
                    display.ClickMode = ClickMode.Rotation;
                else if (colorStrip.HasSelection)
                    display.ClickMode = ClickMode.ColorSet;
                else
                    display.ClickMode = ClickMode.None;
            }
        }

        private void UpdateDisplayedCube()
        {
            foreach (var display in CubeDisplays)
                display.RubiksCube = rubiksCube;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Settings.Instance.Palette = colorStrip.Colors;
            Settings.Instance.CubeColors = rubiksCube.AllColors;
            //Settings.Instance.Save();
            base.OnClosing(e);
        }

        private static DialogResult ShowQuestionPrompt(string message)
        {
            return MessageBox.Show(message, Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void buttonCleanSlate_Click(object sender, EventArgs e)
        {
            if (ShowQuestionPrompt("Are you sure you want to undo changes?") == DialogResult.Yes)
            {
                labellErrorStatus.Text = string.Empty;
                rubiksCube.CleanSlate();
                tableLayoutPanel.Invalidate(true);
            }
        }

        private void buttonUndoChanges_Click(object sender, EventArgs e)
        {
            if (ShowQuestionPrompt("Are you sure you want to undo changes?") == DialogResult.Yes)
            {
                labellErrorStatus.Text = string.Empty;
                rubiksCube.Restore();
                tableLayoutPanel.Invalidate(true);
            }
        }

        private void textBoxCommand_KeyDown(object sender, KeyEventArgs e)
        {

            String Scramble = "D2 U2 R B D F' L2 F2 D U B' U2 D2 L2 B' L' F R L F2 U2 L2 U2 D' L2";
            textBoxCommand.BackColor = Color.FromKnownColor(KnownColor.Window);
            if (e.KeyCode != Keys.Enter) return;
            string lower = textBoxCommand.Text;
            if (lower == "clean slate") rubiksCube.CleanSlate();
            string[] splitted = textBoxCommand.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(textBoxCommand.Text.Contains(',')) splitted = textBoxCommand.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var moveList = new List<CubeMove>();
            if (lower == "scramble")
            {
                splitted = Scramble.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            foreach (string str in splitted)
            {
                try
                {
                    string moveToBeMade = str.Replace("'", "i");
                    if (str.ToLower() == "h")
                    {
                        // UP Inverse and Down 
                        moveList.Add(new CubeMove("Ui"));
                        moveList.Add(new CubeMove("D"));
                    }
                    else if (str.ToLower() == "hi")
                    {
                        // UP and Down Inverse
                        moveList.Add(new CubeMove("U"));
                        moveList.Add(new CubeMove("Di"));
                    }
                    else if (str.ToLower() == "v")
                    {

                        moveList.Add(new CubeMove("L"));
                        moveList.Add(new CubeMove("Ri"));
                    }
                    else if (str.ToLower() == "vi")
                    {

                        moveList.Add(new CubeMove("Li"));
                        moveList.Add(new CubeMove("R"));
                    }
                    else if (str.ToLower() == "rs")
                    {
                        //moveList.Add(new CubeMove(CubeSide.Right, Rotation.Cw));


                    }
                    else if (moveToBeMade.Contains("2"))
                    {
                        string[] vs = moveToBeMade.Split('2');
                        foreach (string s in vs)
                        {
                            moveList.Add(new CubeMove(vs[0]));
                        }
                    }
                    else moveList.Add(new CubeMove(moveToBeMade));
                }
                catch (ArgumentException)
                {
                    textBoxCommand.BackColor = Color.LightPink;
                    return; // Invalid input
                }
            }

            foreach (var move in moveList)
            {
                lblStatus.Text = "Last Move: " + move;
                rubiksCube.MakeMove(move);
            }

            e.SuppressKeyPress = true;
            textBoxCommand.Clear();
            tableLayoutPanel.Invalidate();
        }

        private void checkBoxLockColors_CheckedChanged(object sender, EventArgs e)
        {
            SetHoverEffect();
            colorStrip.Enabled = !checkBoxLockColors.Checked;
        }

        private void colorStrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetHoverEffect();

            foreach (var display in CubeDisplays)
                display.NewColor = colorStrip.SelectedColor;
        }

        private void cubeDisplay_CellMouseClicked(object sender, CellMouseClickedEventArgs e)
        {
            UpdateErrorStatus();
        }

        private void RubiksCubeMoveMade(object sender, CubeMove move)
        {
            if (rubiksCube.Solved)
            {
                lblStatus.Text = "Cube Solved";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                lblStatus.Text = "Last Move: " + move;
            }
        }
    }
}
