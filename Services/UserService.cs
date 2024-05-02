using task_tracker_group.Models;
using task_tracker_group.Models.DTO;
using task_tracker_group.Services.Context;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace task_tracker_group.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string UserName)
        {
            //check if username exist
            // if 1 matches then return th eitem
            //if no item matche sthen return null

            return _context.UserInfo.SingleOrDefault(user => user.Username == UserName) != null;
        }
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            bool result = false;

            //if userdoesnt exist , add user
            if (!DoesUserExist(UserToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var hashPassword = HashPassword(UserToAdd.Password);

                Random random = new Random();
                int randomNumber = random.Next(1, 13);

                newUser.ID = UserToAdd.ID;
                newUser.Username = UserToAdd.Username;
                newUser.ColorId = randomNumber;
                newUser.DateMade = DateTime.Today;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                //add newuser to the stabase
                _context.Add(newUser);

                //save into database , erturn of number of entries writting into database
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        // HashedPassword = H(Salt + Password)
        public PasswordDTO HashPassword(string password)
        {
            PasswordDTO newHashPassword = new PasswordDTO();

            // create a byte using 64 bytes of salt
            byte[] SaltByte = new byte[64];

            // this is our randomizer 
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            //take our saltbyte and make sure it contains zeros. making it secure.
            provider.GetNonZeroBytes(SaltByte);

            //converting our salt
            string salt = Convert.ToBase64String(SaltByte);

            //encode password with salt into our hash. 10000 times where 10000 is a common starting point
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            //convert the resulting byte array as a string of 256 bytes
            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            //we can save our hash and salt into our passwordDTO
            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;

            return newHashPassword;

        }

        //verify user password
        public bool VerifyUsersPasswords(string? password, string? storedHash, string? storedSalt)
        {
            //encode salt back in the originial byte array 
            byte[] SaltBytes = Convert.FromBase64String(storedSalt);

            //repeat process of taking password entered and hashing it 
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltBytes, 10000);

            //RRC289 object, retireve the 256 bytes of hash, convert those into a string, assigns a result to the newHash
            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;
        }
        public IActionResult Login(LoginDTO User)
        {
            IActionResult Result = Unauthorized();
            //check if user exist
            if (DoesUserExist(User.Username))
            {
                //if continue with authentication
                // if true store our user object
                UserModel foundUser = GetUserByUsername(User.Username);
                if (VerifyUsersPasswords(User.Password, foundUser.Hash, foundUser.Salt))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(), // Claims can be added here if needed
                        expires: DateTime.Now.AddMinutes(30), // Set token expiration time (e.g., 30 minutes)
                        signingCredentials: signinCredentials // Set signing credentials
                    );

                    // Generate JWT token as a string
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);


                    Result = Ok(new { Token = tokenString });
                }
                //Token 
                //uwihfasuihfeuie. = header
                //upiroenvjdsaewf. = Payload: contains claims such as exporation time 
                //iuroisdmcweeeef. = Signature encrypts and combines header and payload using secret key


            }
            return Result;
        }
        public UserModel GetUserByUsername(string username)
        {
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }
        public bool UpdateUser(UserModel userToUpdate)
        {
            _context.Update<UserModel>(userToUpdate);
            return _context.SaveChanges() != 0;
        }
        public bool UpdateUsername(int id, string username)
        {
            //sending over just the username 
            // we have to get the object to be updated

            UserModel foundUser = GetUserById(id);
            bool result = false;

            if (foundUser != null)
            {
                // a user was found
                //update founduser object
                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }
        public UserModel GetUserById(int id)
        {
            return _context.UserInfo.SingleOrDefault(user => user.ID == id);
        }
        public bool DeleteUser(string userToDelete)
        {
            // we are only seeing over the username 
            // if username found delete user
            UserModel foundUser = GetUserByUsername(userToDelete);

            bool result = false;

            if (foundUser != null)
            {
                // user was found 
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        public UserIdDTO GetUserIdDTOByUsername(string username)
        {

            UserIdDTO UserInfo = new UserIdDTO();
            //query through database to find the user 
            UserModel foundUser = _context.UserInfo.SingleOrDefault(user => user.Username == username);
            UserInfo.UserId = foundUser.ID;

            UserInfo.PublishedName = foundUser.Username;

            return UserInfo;
        }

        public IActionResult UpdateUserInfo(UserModel userObject)
        {

            UserModel existingUser = GetUserById(userObject.ID);

            if (existingUser != null)
            {

                existingUser.Image = userObject.Image;
                _context.SaveChanges();
            }
            return Ok(existingUser);
        }

        public bool UpdateUserColor(int userId, int colorId)
        {
            UserModel existingUser = GetUserById(userId);

            if (existingUser != null)
            {

                existingUser.ColorId = colorId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IActionResult UpdateUserImage(UserModel updateUser)
        {

            UserModel existingUser = GetUserById(updateUser.ID);

            if (existingUser != null)
            {
                existingUser.Image = updateUser.Image;
                _context.SaveChanges();
            }
            return Ok(existingUser);
        }

        public UserModel GetProfileByUserID(int id)
        {
            return _context.UserInfo.SingleOrDefault(item => item.ID == id);
        }




    }
}
//  public int ID { get; set; }

//     public string? Username { get; set; }

//     public string? ProfilePicture { get; set; }

//     public int? ColorId { get; set; }

//     public DateTime? DateMade { get; set; }

//     public string? Salt { get; set; }

//     public string? Hash { get; set; }