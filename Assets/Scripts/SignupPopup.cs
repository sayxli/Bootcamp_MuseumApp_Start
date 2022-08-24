using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuseumApp
{
    public class SignupPopup : MonoBehaviour
    {
        private static readonly int ExitHash = Animator.StringToHash("Exit");

        public TMP_InputField usernameInput;
        public int minUsernameCharacters = 3;
        private Image usernameHolderImage;

        public TMP_InputField passwordInput;
        public int minPasswordCharacters = 8;
        private Image passwordHolderImage;

        public Color wrongInputFieldColor = new Color(1, 0.82f, 0.82f);

        public Animator animator;

        public void OnRegisterClicked()
        {
            var usernameValid = IsInputValid(usernameInput, minUsernameCharacters);
            var passwordValid = IsInputValid(passwordInput, minPasswordCharacters);

            usernameHolderImage.color = usernameValid ? Color.white : wrongInputFieldColor;
            passwordHolderImage.color = passwordValid ? Color.white : wrongInputFieldColor;

            if (!usernameValid || !passwordValid)
                return;

            // TODO: Register player
            //because database is static class you can just call it here
            Database.RegisterPlayer(usernameInput.text, passwordInput.text);
            //we can user playerprefs to keep track of who is logged in 

            Login();
            ClosePopup();
        }

        public void OnLoginClicked()
        {
            // TODO: Check credentials
            //we have to create a user using our database data 
            var user = Database.GetUser(usernameInput.text);
            if(user == null)
            {
                //if there is no username, switch to a wrong image and that image's color goes to say its wrong
                usernameHolderImage.color = wrongInputFieldColor;
                passwordHolderImage.color = Color.white;

            }
            else if (user.Password != passwordInput.text) //if the password is wrong
            {
                usernameHolderImage.color = Color.white;
                passwordHolderImage.color = wrongInputFieldColor;

            }

            Login();
            ClosePopup();
        }

        private void Login()
        {
            // TODO: log in user
            User.Login(usernameInput.text);
        }

        private void ClosePopup()
        {
            animator.SetTrigger(ExitHash);
            FindObjectOfType<HomeScreen>().Refresh();
        }

        private void OnFinishedExitAnimation()
        {
            SceneManager.UnloadSceneAsync("SignupPopup");
        }

        private bool IsInputValid(TMP_InputField inputField, int minCharacters)
        {
            return !string.IsNullOrEmpty(inputField.text) && inputField.text.Length >= minCharacters;
        }

        private void Awake()
        {
            usernameHolderImage = usernameInput.GetComponent<Image>();
            passwordHolderImage = passwordInput.GetComponent<Image>();
        }
    }
}