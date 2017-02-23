using System.Windows.Forms;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Contains Function or routine to read and write the user preferences xml i.e. Preferences.xml.
    /// </summary>
    public class Preferences
    {
        private static DataSecurity security = new DataSecurity();
        private static XMLHelper xmlHelper = new XMLHelper();
        private const string PASSWORD_KEY = "password";
        private const string USERNAME_KEY = "username";
        private const string REMEMBER_ME_KEY = "rememberme";
        private const string SCANNING_RESOLUTION_KEY = "scanningResolution";
        private const string FILE_SAVE_RESOLUTION_KEY = "filesaveresolution";
        private const string DUPLEX_SCANNING_ENABLED_KEY = "duplexenabled";
        private const string DOCUMENT_FEEDER_ENABLED_KEY = "feederenabled";
        private const string LAST_LOGGED_IN_USER_KEY = "lastuser";
        private const string LAST_LOGIN_DATE_KEY = "lastlogindate";
        private const string IMAGE_SIZE_KEY = "imagesize";
        private const string XFER_COUNT_KEY = "xfercount";

        /// <summary>
        /// List of preferences
        /// </summary>
        public enum Preference
        {
            RememberMe,
            Username,
            Password,
            LastUser,
            LastLoginDate
        }

        #region Public Methods

        /// <summary>
        /// Saves the preference value passed.
        /// </summary>
        /// <param name="preference">Preference fro which value needs to be saved.</param>
        /// <param name="value">Value</param>
        public static void SavePreference(Preference preference, string value)
        {
            switch (preference)
            {
                case Preference.Username:
                    xmlHelper.SetValue(USERNAME_KEY, security.Encrypt(value));
                    break;

                case Preference.Password:
                    xmlHelper.SetValue(PASSWORD_KEY, security.Encrypt(value));
                    break;

                case Preference.LastUser:
                    xmlHelper.SetValue(LAST_LOGGED_IN_USER_KEY, security.Encrypt(value));
                    break;

                case Preference.LastLoginDate:
                    xmlHelper.SetValue(LAST_LOGIN_DATE_KEY, value);
                    break;

                case Preference.RememberMe:
                    xmlHelper.SetValue(REMEMBER_ME_KEY, value);
                    break;
            }
        }

        /// <summary>
        /// Clear credentials stored into Preferences.xml file
        /// </summary>
        public static void ClearCredentials()
        {
            xmlHelper.SetValue(REMEMBER_ME_KEY, "false");
            xmlHelper.SetValue(USERNAME_KEY, string.Empty);
            xmlHelper.SetValue(PASSWORD_KEY, string.Empty);
            xmlHelper.Save();
        }

        /// <summary>
        /// Save changes made to Preference.xml
        /// </summary>
        public static void Save()
        {
            xmlHelper.Save();
        }

        /// <summary>
        /// Reads the Preference.xml file and returns the value stored.
        /// </summary>
        /// <param name="preference">Preference for which stored value is needed.</param>
        /// <returns>Preference value stored</returns>
        public static string GetPreference(Preference preference)
        {
            var value = string.Empty;
            switch (preference)
            {
                case Preference.Username:
                    value = xmlHelper.GetValue(USERNAME_KEY);
                    value = !string.IsNullOrEmpty(value) ? security.Decrypt(value) : string.Empty;
                    break;

                case Preference.Password:
                    value = xmlHelper.GetValue(PASSWORD_KEY);
                    value = !string.IsNullOrEmpty(value) ? security.Decrypt(value) : string.Empty;
                    break;

                case Preference.RememberMe:
                    value = xmlHelper.GetValue(REMEMBER_ME_KEY);
                    break;

                case Preference.LastUser:
                    value = xmlHelper.GetValue(LAST_LOGGED_IN_USER_KEY);
                    value = !string.IsNullOrEmpty(value) ? security.Decrypt(value) : string.Empty;
                    break;

                case Preference.LastLoginDate:
                    value = xmlHelper.GetValue(LAST_LOGIN_DATE_KEY);
                    break;
            }

            return value;
        }

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        /// Gets PictureBoxSizeMode for the picture box in which scanned image is shown.
        /// </summary>
        public static PictureBoxSizeMode ImageSize
        {
            get
            {
                var imageSizeMode = PictureBoxSizeMode.StretchImage;
                var value = xmlHelper.GetValue(IMAGE_SIZE_KEY);

                if (string.IsNullOrEmpty(value)) return imageSizeMode;
                switch (value)
                {
                    case "AutoSize":
                        imageSizeMode = PictureBoxSizeMode.AutoSize;
                        break;

                    case "CenterImage":
                        imageSizeMode = PictureBoxSizeMode.CenterImage;
                        break;

                    case "Normal":
                        imageSizeMode = PictureBoxSizeMode.Normal;
                        break;

                    case "StretchImage":
                        imageSizeMode = PictureBoxSizeMode.StretchImage;
                        break;

                    case "Zoom":
                        imageSizeMode = PictureBoxSizeMode.Zoom;
                        break;

                    default:
                        imageSizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                }

                return imageSizeMode;
            }
        }

        #endregion Public Properties
    }
}