using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CluelessControl
{
    public partial class TVScreenForm : Form
    {
        public TVScreenForm()
        {
            InitializeComponent();
        }

        private void TVScreenForm_Load(object sender, EventArgs e)
        {
            TryToLoadBackgroundImage();
            AddEvents();
        }

        private void TryToLoadBackgroundImage()
        {
            try
            {
                Image backgroundImage = Image.FromFile("img/background.jpg");
                BackgroundImage = backgroundImage;
            }
            catch
            {
                // If there's no background image, just exit.
            }
        }

        private void AddEvents()
        {
            var gameState = GameState.Instance;

            gameState.EventClearQuestion += GameState_EventClearQuestion;
            gameState.EventShowQuestion += GameState_EventShowQuestion;
            gameState.EventShowAnswers += GameState_EventShowAnswers;
            gameState.EventAnswerSelected += GameState_EventAnswerSelected;
            gameState.EventCorrectAnswerShown += GameState_EventCorrectAnswerShown;
            gameState.EventRefreshEnvelopes += GameState_EventRefreshEnvelopes;
            gameState.EventStartTrading += GameState_EventStartTrading;
            gameState.EventRefreshOffer += GameState_EventRefreshOffer;
            gameState.EventGameOver += GameState_EventGameOver;
        }

        private void GameState_EventClearQuestion(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventShowQuestion(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventShowAnswers(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventAnswerSelected(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventCorrectAnswerShown(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventRefreshEnvelopes(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventStartTrading(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventRefreshOffer(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GameState_EventGameOver(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Form Closing
        private void TVScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.WindowsShutDown:
                case CloseReason.TaskManagerClosing:
                case CloseReason.ApplicationExitCall:
                    break;
                default:
                    MessageBox.Show(Constants.CLOSE_ON_DIRECTOR_FORM_MESSAGE, Constants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    break;
            }
        }
        #endregion
    }
}
