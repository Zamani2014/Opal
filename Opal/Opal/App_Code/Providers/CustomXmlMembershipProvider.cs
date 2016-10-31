using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;

namespace Opal.Providers
{
    /// <summary>
    /// Specialized MembershipProvider that uses a file (Users.config) to store its data.
    /// Passwords for the users are always stored as a salted hash (see: http://msdn.microsoft.com/msdnmag/issues/03/08/SecurityBriefs/)
    /// </summary>
    public class CustomXmlMembershipProvider : MembershipProvider
    {
        private string _applicationName;
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minRequiredPasswordLength;
        private string _passwordStrengthRegularExpression;
        private bool _enablePasswordReset;
        private bool _requiresUniqueEmail;

        private DataTable _users;

        private const string _cUserFilename = "Users.config";
        private const string _cProviderName = "CustomXmlMembershipProvider";
        private String _path = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/{0}", _cUserFilename));

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            name = _cProviderName;

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Xml membership provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            _applicationName = getConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _maxInvalidPasswordAttempts = Convert.ToInt32(getConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _passwordAttemptWindow = Convert.ToInt32(getConfigValue(config["passwordAttemptWindow"], "10"));
            _minRequiredNonAlphanumericCharacters = Convert.ToInt32(getConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(getConfigValue(config["minRequiredPasswordLength"], "7"));
            _passwordStrengthRegularExpression = Convert.ToString(getConfigValue(config["passwordStrengthRegularExpression"], ""));
            _enablePasswordReset = Convert.ToBoolean(getConfigValue(config["enablePasswordReset"], bool.TrueString));
            _requiresUniqueEmail = Convert.ToBoolean(getConfigValue(config["requiresUniqueEmail"], bool.TrueString));
            
            //load/create the usertable
            if (File.Exists(_path))
            {
                lock (_path)
                {
                    _users = new DataTable("UserTable");
                    _users.ReadXml(_path);
                }
            }
            else
            {
                _users = new DataTable("UserTable");
                _users.Columns.AddRange(new DataColumn[] {
                    new DataColumn("PKID", typeof(Guid)),
                    new DataColumn("FirstName", typeof(string)),
                    new DataColumn("LastName", typeof(string)),
                    new DataColumn("NationalCode", typeof(Int64)),
                    new DataColumn("IntroduceWay", typeof(string)),
                    new DataColumn("Concessionaire", typeof(string)),
                    new DataColumn("GradeLevel", typeof(string)),
                    new DataColumn("CompanyName", typeof(string)),
                    new DataColumn("RegisterNo", typeof(int)),
                    new DataColumn("ActivityField", typeof(string)),
                    new DataColumn("PostInCo", typeof(string)),
                    new DataColumn("Website", typeof(string)),
                    new DataColumn("CompanyEmail", typeof(string)),
                    new DataColumn("Tel", typeof(Int64)),
                    new DataColumn("Mobile", typeof(Int64)),
                    new DataColumn("PostalCode", typeof(string)),
                    new DataColumn("Address", typeof(string)),
                    new DataColumn("Username", typeof(string)),
                    new DataColumn("ApplicationName", typeof(string)),
                    new DataColumn("Email", typeof(string)),
                    new DataColumn("Comment", typeof(string)),
                    new DataColumn("AgentName", typeof(string)),
                    new DataColumn("AgentTelNo", typeof(Int64)),
                    new DataColumn("LineNo", typeof(Int64)),
                    new DataColumn("Charge", typeof(Int64)),
                    new DataColumn("Credit", typeof(Int64)),
                    new DataColumn("SendFee", typeof(Int64)),
                    new DataColumn("ReceiveFee", typeof(Int64)),
                    new DataColumn("Facilities", typeof(string)),
                    new DataColumn("OldSMS", typeof(string)),
                    new DataColumn("IsSubscriber", typeof(bool)),
                    new DataColumn("Salt", typeof(string)),
                    new DataColumn("Password", typeof(string)),
                    new DataColumn("IsApproved", typeof(bool)),
                    new DataColumn("LastActivityDate", typeof(DateTime)),
                    new DataColumn("LastLoginDate", typeof(DateTime)),
                    new DataColumn("LastPasswordChangedDate", typeof(DateTime)),
                    new DataColumn("CreationDate", typeof(DateTime)),
                    new DataColumn("IsOnLine", typeof(bool)),
                    new DataColumn("IsLockedOut", typeof(bool)),
                    new DataColumn("LastLockedOutDate", typeof(DateTime)),
                    new DataColumn("FailedPasswordAttemptCount", typeof(int)),
                    new DataColumn("FailedPasswordAttemptWindowStart", typeof(DateTime)),
                    new DataColumn("FailedPasswordAnswerAttemptCount", typeof(int)),
                    new DataColumn("FailedPasswordAnswerAttemptWindowStart", typeof(DateTime))
                    }
                );
                _users.AcceptChanges();
                save();
            }
        }

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (ValidateUser(username, oldPassword))
            {
                ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, false);
                OnValidatingPassword(args);
                if (args.Cancel)
                {
                    if(args.FailureInformation != null)
                        throw args.FailureInformation;
                    else
                        throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
                }
                DataRow row = _users.Select(string.Format("Username='{0}'", username))[0];

                SaltedHash sh = SaltedHash.Create(newPassword);
                row["Salt"] = sh.Salt;
                row["Password"] = sh.Hash;
                row["LastPasswordChangedDate"] = DateTime.Now;
                row.AcceptChanges();
                save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return this.CreateUser(
                username,
                password,
                email,
                passwordQuestion,
                passwordAnswer,
                isApproved,
                providerUserKey,
                out status,
                "",
                "",
                0,
                "",
                "",
                "",
                "",
                0,
                "",
                "",
                "",
                "",
                0, 0, 0, "", "", 0, 0, 0, 0, 0, 0, "", "", false );
        }

        public OurMembershipUser CreateUser(
            string username, 
            string password, 
            string email, 
            string passwordQuestion, 
            string passwordAnswer, 
            bool isApproved, 
            object providerUserKey, 
            out MembershipCreateStatus status,
            string firstName,
            string lastName,
            Int64 nationalCode,
            string introduceWay,
            string concessionaire,
            string gradeLevel,
            string companyName,
            int registerNo,
            string activityField,
            string postInCo,
            string website,
            string companyEmail,
            Int64 tel,
            Int64 mobile,
            Int64 postalCode,
            string address,
            string agentName,
            Int64 agentTelNo,
            Int64 lineNo,
            Int64 charge,
            Int64 credit,
            Int64 sendFee,
            Int64 receiveFee,
            string facilities,
            string oldSMS,
            bool isSubscriber)
        {
            if (username == string.Empty)
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && GetUserNameByEmail(email) != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            SaltedHash sh = SaltedHash.Create(password);

            MembershipUser u = GetUser(username, false);
            if (u == null)
            {
                _users.Rows.Add(
                    Guid.NewGuid(), //PKID
                    firstName, //FirstName
                    lastName, //LastName
                    nationalCode, // National Code
                    introduceWay, // Introduce Way
                    concessionaire, //Concessionaire
                    gradeLevel, // Grade Level
                    companyName, // Company Name
                    registerNo, // Register Number
                    activityField, // Activity Field
                    postInCo, //PostInCo
                    website, // Website
                    companyEmail, //CompanyEmail
                    tel, // Static Tel Number
                    mobile, // Mobile Number
                    postalCode, //PostalCode
                    address, // Address
                    username, //Username
                    ApplicationName,//ApplicationName
                    email, //Email
                    string.Empty, //Comment
                    agentName, //Agent Name
                    agentTelNo, //Agent Tel No
                    lineNo, // Line No
                    0, //Full Acount Charge
                    0, //Credit Balance
                    0, //SendFee
                    0, //ReceiveFee
                    string.Empty, //Facilities
                    oldSMS, //OldSMS
                    isSubscriber, //Is Subscriber
                    sh.Salt, //salt for the password
                    sh.Hash, //password hash
                    isApproved, //IsApproved
                    //DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now,
                    GetDateTime.GenerateDateTime3(), //LastActivityDate
                    GetDateTime.GenerateDateTime3(), //LastLoginDate
                    GetDateTime.GenerateDateTime3(), //LastPasswordChangedDate
                    GetDateTime.GenerateDateTime3(), //CreationDate
                    false, //IsOnLine
                    false, //IsLockedOut
                    DateTime.MinValue, //LastLockedOutDate
                    0, //FailedPasswordAttemptCount
                    DateTime.MinValue, //FailedPasswordAttemptWindowStart
                    0, //FailedPasswordAnswerAttemptCount
                    DateTime.MinValue //FailedPasswordAnswerAttemptWindowStart
                    );
                _users.AcceptChanges();
                save();
                status = MembershipCreateStatus.Success;

                return (OurMembershipUser)GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if (rows.Length > 0)
            {
                if (deleteAllRelatedData)
                {
                    string[] roles = Roles.GetRolesForUser(username);
                    if (roles.Length > 0)
                        Roles.RemoveUserFromRoles(username, roles);
                }

                _users.Rows.Remove(rows[0]);
                _users.AcceptChanges();
                save();
                return true;
            }
            else
            {
                return false;
            }
        } 

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            if (string.IsNullOrEmpty(emailToMatch))
                throw new ArgumentException("emailToMatch is null or empty", "emailToMatch");
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex must be 0 or greater", "pageIndex");
            if (pageSize < 1)
                throw new ArgumentException("pageSize must be greater than 0", "pageSize");
            
            MembershipUserCollection coll = new MembershipUserCollection();
            DataRow[] rows = _users.Select(string.Format("Email LIKE '{0}'", emailToMatch),"Username ASC");

            for (int i = pageIndex * pageSize; (i < (pageIndex + 1) * pageSize) && (i < rows.Length); i++)
            {
                coll.Add(createMembershipUser(rows[i]));
            }
            totalRecords = rows.Length;
            return coll;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            if (string.IsNullOrEmpty(usernameToMatch))
                throw new ArgumentException("usernameToMatch is null or empty", "usernameToMatch");
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex must be 0 or greater", "pageIndex");
            if (pageSize < 1)
                throw new ArgumentException("pageSize must be greater than 0", "pageSize");

            MembershipUserCollection coll = new MembershipUserCollection();
            DataRow[] rows = _users.Select(string.Format("Username = '{0}'", usernameToMatch), "Username ASC");

            for (int i = pageIndex * pageSize; (i < (pageIndex + 1) * pageSize) && (i < rows.Length); i++)
            {
                coll.Add(createMembershipUser(rows[i]));
            }
            totalRecords = rows.Length;
            return coll;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex must be 0 or greater", "pageIndex");
            if (pageSize < 1)
                throw new ArgumentException("pageSize must be greater than 0", "pageSize");

            MembershipUserCollection coll = new MembershipUserCollection();
            DataRow[] rows = _users.Select(string.Empty, "Username ASC");

            for (int i = pageIndex * pageSize; (i < (pageIndex + 1) * pageSize) && (i < rows.Length); i++)
            {
                coll.Add(createMembershipUser(rows[i]));
            }
            totalRecords = rows.Length;
            return coll;
        }

        public override int GetNumberOfUsersOnline()
        {
            DateTime compareTime = DateTime.Now.Subtract(new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0));
            int usersOnline = 0;
            foreach (DataRow row in _users.Select())
            {
                if (((DateTime)row["LastActivityDate"]) > compareTime)
                    usersOnline++;
            }
            return usersOnline;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if (rows.Length > 0)
            {
                if (userIsOnline)
                {
                    rows[0]["LastActivityDate"] = DateTime.Now;
                    rows[0]["IsOnline"] = true;
                    rows[0].AcceptChanges();
                    save();
                }
                return createMembershipUser(rows[0]);
            }
            else
            {
                return null;
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            foreach(DataRow row in _users.Rows)
            {
                if (row["PKID"].Equals(providerUserKey))
                {
                    if (userIsOnline)
                    {
                        row["LastActivityDate"] = DateTime.Now;
                        row["IsOnline"] = true;
                        row.AcceptChanges();
                        save();
                    }
                    return createMembershipUser(row);
                }                    
            }
            return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            DataRow[] rows = _users.Select(string.Format("Email='{0}'", email));
            if (rows.Length > 0)
                return (string)rows[0]["Email"];
            else
                return null;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotSupportedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
                throw new NotSupportedException();

            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if (rows.Length > 0)
            {
                string newPassword = Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);
                SaltedHash sh = SaltedHash.Create(newPassword);
                rows[0]["Salt"] = sh.Salt;
                rows[0]["Password"] = sh.Hash;
                rows[0]["LastPasswordChangedDate"] = DateTime.Now;
                rows[0].AcceptChanges();
                save();
                return newPassword;
            }
            else
            {
                return null;
            }
        }

        public override bool UnlockUser(string userName)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", userName));
            if (rows.Length > 0)
            {
                rows[0]["IsLockedOut"] = false;
                rows[0].AcceptChanges();
                save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void UpdateUser(MembershipUser user)
        {
            OurMembershipUser u = (OurMembershipUser)user;
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", u.UserName));
            if (rows.Length > 0)
            {
                rows[0]["Email"] = u.Email;
                rows[0]["FirstName"] = u.FirstName;
                rows[0]["LastName"] = u.LastName;
                rows[0]["NationalCode"] = u.NationalCode;
                rows[0]["IntroduceWay"] = u.IntroduceWay;
                rows[0]["Concessionaire"] = u.Concessionaire;
                rows[0]["GradeLevel"] = u.GradeLevel;
                rows[0]["CompanyName"] = u.CompanyName;
                rows[0]["RegisterNo"] = u.RegisterNo;
                rows[0]["ActivityField"] = u.ActivityField;
                rows[0]["PostInCo"] = u.PostInCo;
                rows[0]["Website"] = u.Website;
                rows[0]["CompanyEmail"] = u.CompanyEmail;
                rows[0]["Tel"] = u.Tel;
                rows[0]["Mobile"] = u.Mobile;
                rows[0]["PostalCode"] = u.PostalCode;
                rows[0]["Address"] = u.Address;
                rows[0]["AgentName"] = u.AgentName;
                rows[0]["AgentTelNo"] = u.AgentTelNo;
                rows[0]["OldSMS"] = u.OldSMS;
                rows[0]["IsSubscriber"] = u.IsSubscriber;
                rows[0]["Comment"] = u.Comment;
                rows[0]["IsApproved"] = u.IsApproved;
                rows[0]["IsLockedOut"] = u.IsLockedOut;
                rows[0]["CreationDate"] = u.CreationDate;
                rows[0]["LastLoginDate"] = u.LastLoginDate;
                rows[0]["LastActivityDate"] = u.LastActivityDate;
                rows[0]["LastPasswordChangedDate"] = u.LastPasswordChangedDate;
                rows[0]["LastLockedOutDate"] = u.LastLockoutDate;
                rows[0]["IsOnline"] = u.IsOnline;
                rows[0]["IsLockedOut"] = u.IsLockedOut;
                rows[0]["Credit"] = u.Credit;
                rows[0]["LineNo"] = u.LineNo;
                rows[0]["Charge"] = u.Charge;
                rows[0]["SendFee"] = u.SendFee;
                rows[0]["ReceiveFee"] = u.ReceiveFee;
                rows[0]["Facilities"] = u.Facilities;

                rows[0].AcceptChanges();
                save();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            DataRow[] rows = _users.Select(string.Format("Username='{0}'", username));
            if ((rows.Length > 0) && ((bool)rows[0]["IsApproved"] == true))
            {
                SaltedHash sh = SaltedHash.Create((string)rows[0]["Salt"], (string)rows[0]["Password"]);
                if (sh.Verify(password))
                {
                    rows[0]["LastLoginDate"] = DateTime.Now;
                    rows[0]["LastActivityDate"] = DateTime.Now;
                    rows[0].AcceptChanges();
                    save();
                    return true;
                }
            }
            return false;
        }

        #region private methods
        private OurMembershipUser createMembershipUser(DataRow row)
        {
            return new OurMembershipUser(
                        _cProviderName,
                        (string)row["Username"],
                        (Guid)row["PKID"],
                        (string)row["Email"],
                        string.Empty,
                        (string)row["Comment"],
                        (bool)row["IsApproved"],
                        (bool)row["IsLockedOut"],
                        (DateTime)row["CreationDate"],
                        (DateTime)row["LastLoginDate"],
                        (DateTime)row["LastActivityDate"],
                        (DateTime)row["LastPasswordChangedDate"],
                        (DateTime)row["LastLockedOutDate"],
                        (string)row["FirstName"],
                        (string)row["LastName"],
                        (Int64)row["NationalCode"],
                        (string)row["IntroduceWay"],
                        (string)row["Concessionaire"],
                        (string)row["GradeLevel"],
                        (string)row["CompanyName"],
                        (int)row["RegisterNo"],
                        (string)row["ActivityField"],
                        (string)row["PostInCo"],
                        (string)row["Website"],
                        (string)row["CompanyEmail"],
                        (Int64)row["Tel"],
                        (Int64)row["Mobile"],
                        (Int64)row["PostalCode"],
                        (string)row["Address"],
                        (string)row["AgentName"],
                        (Int64)row["AgentTelNo"],
                        (Int64)row["LineNo"],
                        (Int64)row["Charge"],
                        (Int64)row["Credit"],
                        (Int64)row["SendFee"],
                        (Int64)row["ReceiveFee"],
                        (string)row["Facilities"],
                        (string)row["OldSMS"],
                        (bool)row["IsSubscriber"]
                    );
        }

        private string getConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;
            else
                return configValue;
        }

        private void save()
        {
            lock (_path)
            {
                _users.WriteXml(_path, XmlWriteMode.WriteSchema);
            }
        }
        #endregion
    }

    #region SaltedHash Class
    public sealed class SaltedHash
    {
        private readonly string _salt;
        private readonly string _hash;
        private const int saltLength = 6;

        public string Salt { get { return _salt; } }
        public string Hash { get { return _hash; } }

        public static SaltedHash Create(string password)
        {
            string salt = _createSalt();
            string hash = _calculateHash(salt, password);
            return new SaltedHash(salt, hash);
        }

        public static SaltedHash Create(string salt, string hash)
        {
            return new SaltedHash(salt, hash);
        }

        public bool Verify(string password)
        {
            string h = _calculateHash(_salt, password);
            return _hash.Equals(h);
        }

        private SaltedHash(string s, string h)
        {
            _salt = s;
            _hash = h;
        }

        private static string _createSalt()
        {
            byte[] r = _createRandomBytes(saltLength);
            return Convert.ToBase64String(r);
        }

        private static byte[] _createRandomBytes(int len)
        {
            byte[] r = new byte[len];
            new RNGCryptoServiceProvider().GetBytes(r);
            return r;
        }

        private static string _calculateHash(string salt, string password)
        {
            byte[] data = _toByteArray(salt + password);
            byte[] hash = _calculateHash(data);
            return Convert.ToBase64String(hash);
        }

        private static byte[] _calculateHash(byte[] data)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(data);
        }

        private static byte[] _toByteArray(string s)
        {
            return System.Text.Encoding.UTF8.GetBytes(s);
        }
    }
    #endregion

    #region OurMembershipUser Class
    public class OurMembershipUser : MembershipUser
    {
        private string _firstName;
        private string _lastName;
        private Int64 _nationalCode;
        private string _introduceWay;
        private string _concessionaire;
        private string _gradeLevel;
        private string _companyName;
        private int _registerNo;
        private string _activityField;
        private string _postInCo;
        private string _website;
        private string _companyEmail;
        private Int64 _tel;
        private Int64 _mobile;
        private Int64 _postalCode;
        private string _address;
        private string _agentName;
        private Int64 _agentTelNo;
        private Int64 _lineNo;
        private Int64 _charge;
        private Int64 _credit;
        private Int64 _sendFee;
        private Int64 _receiveFee;
        private string _facilities;
        private string _oldSMS;
        private bool _IsSubscriber;

        public string IntroduceWay
        {
            get { return _introduceWay; }
            set { _introduceWay = value; }
        }

        public Int64 NationalCode
        {
            get { return _nationalCode; }
            set { _nationalCode = value; }
        }

        public Int64 PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        public string CompanyEmail
        {
            get { return _companyEmail; }
            set { _companyEmail = value; }
        }
        
        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }
        
        public string PostInCo
        {
            get { return _postInCo; }
            set { _postInCo = value; }
        }
        
        public string ActivityField
        {
            get { return _activityField; }
            set { _activityField = value; }
        }
        
        public int RegisterNo
        {
            get { return _registerNo; }
            set { _registerNo = value; }
        }
        
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        
        public string GradeLevel
        {
            get { return _gradeLevel; }
            set { _gradeLevel = value; }
        }
        
        public string Facilities
        {
            get { return _facilities; }
            set { _facilities = value; }
        }

        public Int64 ReceiveFee
        {
            get { return _receiveFee; }
            set { _receiveFee = value; }
        }

        public Int64 SendFee
        {
            get { return _sendFee; }
            set { _sendFee = value; }
        }

        public Int64 Credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

        public Int64 Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public Int64 Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        public Int64 Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        public Int64 LineNo
        {
            get { return _lineNo; }
            set { _lineNo = value; }
        }
        
        public string Concessionaire
        {
            get { return _concessionaire; }
            set { _concessionaire = value; }
        }
        
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public bool IsSubscriber
        {
            get { return _IsSubscriber; }
            set { _IsSubscriber = value; }
        }

        public Int64 AgentTelNo
        {
            get { return _agentTelNo; }
            set { _agentTelNo = value; }
        }

        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value; }
        }

        public string OldSMS
        {
            get { return _oldSMS; }
            set { _oldSMS = value; }
        }

        public OurMembershipUser(
                                  string providername,
                                  string username,
                                  object providerUserKey,
                                  string email,
                                  string passwordQuestion,
                                  string comment,
                                  bool isApproved,
                                  bool isLockedOut,
                                  DateTime creationDate,
                                  DateTime lastLoginDate,
                                  DateTime lastActivityDate,
                                  DateTime lastPasswordChangedDate,
                                  DateTime lastLockedOutDate,
                                 string firstName,
                                 string lastName,
                                 Int64 nationalCode,
                                 string introduceWay,
                                 string concessionaire,
                                 string gradeLevel,
                                 string companyName,
                                 int registerNo,
                                 string activityField,
                                 string postInCo,
                                 string website,
                                 string companyEmail,
                                 Int64 tel,
                                 Int64 mobile,
                                 Int64 postalCode,
                                 string address,
                                 string agentName,
                                 Int64 agentTelNo,
                                 Int64 lineNo,
                                 Int64 charge,
                                 Int64 credit,
                                 Int64 sendFee,
                                 Int64 receiveFee,
                                 string facilities,
                                 string oldSMS,
                                 bool isSubscriber) :
                                                                  base(providername,
                                                                       username,
                                                                       providerUserKey,
                                                                       email,
                                                                       passwordQuestion,
                                                                       comment,
                                                                       isApproved,
                                                                       isLockedOut,
                                                                       creationDate,
                                                                       lastLoginDate,
                                                                       lastActivityDate,
                                                                       lastPasswordChangedDate,
                                                                       lastLockedOutDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.NationalCode = nationalCode;
            this.IntroduceWay = introduceWay;
            this.Concessionaire = concessionaire;
            this.GradeLevel = gradeLevel;
            this.CompanyName = companyName;
            this.RegisterNo = registerNo;
            this.ActivityField = activityField;
            this.PostInCo = postInCo;
            this.Website = website;
            this.CompanyEmail = companyEmail;
            this.Tel = tel;
            this.Mobile = mobile;
            this.PostalCode = postalCode;
            this.Address = address;
            this.AgentName = agentName;
            this.AgentTelNo = agentTelNo;
            this.LineNo = lineNo;
            this.Charge = charge;
            this.Credit = credit;
            this.SendFee = sendFee;
            this.ReceiveFee = receiveFee;
            this.Facilities = facilities;
            this.OldSMS = oldSMS;
            this.IsSubscriber = isSubscriber;
        }
    }
#endregion 
}
