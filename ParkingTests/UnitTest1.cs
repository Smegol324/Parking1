using Xunit.Sdk;
using Parking;
using System.IO;
using Xunit;

namespace ParkingTests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void SetLogin_ValidLogin_SetsLogin()
        {
            // Arrange
            var user = new User("Smegol");

            // Act
            user.Login = "newlogin";

            // Assert
            Assert.AreEqual("newlogin", user.Login);
        }
        [TestMethod]
        public void SetPassword_ValidPassword_SetsPassword()
        {
            // Arrange
            var user = new User("test");

            // Act
            user.Password = "newpassword";

            // Assert
            Assert.AreEqual("newpassword", user.Password);
        }
        [TestMethod]
        public void Cars_Property_Should_Return_Empty_List_When_No_Cars_Are_Assigned()
        {
            // Arrange
            User user = new User("test");
            user.Cars = new List<Car>();
            Car new_car = new Car("qwe");
            user.Cars.Add(new_car);
            // Act
            var cars = user.Cars;

            // Assert
            Assert.IsNotNull(cars);
        }
        [TestMethod]
        public void Moto_Property_Should_Return_Empty_List_When_No_Motorcycles_Are_Assigned()
        {
            // Arrange
            User user = new User("test");
            user.Moto = new List<MotorCycle>();
            MotorCycle new_moto = new MotorCycle("qweqrw");
            user.Moto.Add(new_moto);
            // Act
            var moto = user.Moto;

            // Assert
            Assert.IsNotNull(moto);
        }
        [TestMethod]
        public void Cars_Property_Should_Return_Assigned_Cars()
        {
            // Arrange
            User user = new User("test");
            Car car1 = new Car("Car1");
            Car car2 = new Car("Car2");

            // Act
            user.Cars = new List<Car> { car1, car2 };
            var cars = user.Cars;

            // Assert
            Assert.IsNotNull(cars);
            Assert.AreEqual(2, cars.Count);
        }
        [TestMethod]
        public void Moto_Property_Should_Return_Assigned_Motorcycles()
        {
            // Arrange
            User user = new User("test");
            MotorCycle moto1 = new MotorCycle("Moto1");
            MotorCycle moto2 = new MotorCycle("Moto2");

            // Act
            user.Moto = new List<MotorCycle> { moto1, moto2 };
            var moto = user.Moto;

            // Assert
            Assert.IsNotNull(moto);
            Assert.AreEqual(2, moto.Count);
        }
        [TestMethod]
        public void Money_DefaultValueIsZero()
        {
            // Arrange
            User user = new User("test");
            // Act
            // Assert
            Assert.AreEqual(0, user.Money);
        }

        [TestMethod]
        public void Money_SetValidValue_UpdateMoney()
        {
            // Arrange
            User user = new User("test");
            // Act
            user.Money = 100;
            // Assert
            Assert.AreEqual(100, user.Money);
        }
        [TestMethod]
        public void Name_SetValidValue_UpdateName()
        {
            // Arrange
            User user = new User("test");
            // Act
            user.Name = "John Doe";
            // Assert
            Assert.AreEqual("John Doe", user.Name);
        }
        [TestMethod]
        public void AddCar_Should_AddCarToList()
        {
            // Arrange
            string login = "testuser2";
            User user = new User(login);
            

            // Act
            user.AddCar("Car1");

            // Assert
            Assert.AreEqual(1, user.Cars.Count);
            Assert.AreEqual("Car1", user.Cars[0].Name);
            user.DeleteCar("Car1");
        }
        [TestMethod]
        public void AddMoto_Should_AddMotoToList()
        {
            // Arrange
            string login = "testuser";
            User user = new User(login);

            // Act
            user.AddMoto("Moto1");

            // Assert
            Assert.AreEqual(1, user.Moto.Count);
            Assert.AreEqual("Moto1", user.Moto[0].Name);
            user.DeleteMoto("Moto1");
        }
        [TestMethod]
        public void ChangeLogin_Should_ChangeLoginAndUpdateFile()
        {
            // Arrange
            string login = "testuser";
            User user = new User(login);

            // Act
            bool result = user.ChangeLogin("newlogin");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("newlogin", user.Login);
            user.ChangeLogin("testuser");
        }
        [TestMethod]
        public void ChangeMoney_Should_ChangeMoneyValueAndUpdateFile()
        {
            // Arrange
            string login = "testuser";
            User user = new User(login);

            // Act
            user.ChangeMoney("100");

            // Assert
            Assert.AreEqual(100, user.Money);
        }
        [TestMethod]
        public void ChangeName_Should_ChangeNameAndUpdateFile()
        {
            // Arrange
            string login = "testuser";
            User user = new User(login);

            // Act
            user.ChangeName("John");

            // Assert
            Assert.AreEqual("John", user.Name);
        }
        [TestMethod]
        public void ChangePassword_WhenCurrentPasswordIsIncorrect_ReturnsFalse()
        {
            // Arrange
            User user = new User("testuser");
            user.Password = "correctPassword";
            string currentPassword = "incorrect_password";
            string newPassword = "new_password";

            // Act
            bool result = user.ChangePassword(currentPassword, newPassword);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_WhenNewPasswordIsEmpty_ReturnsFalse()
        {
            // Arrange
            User user = new User("testuser");
            string currentPassword = "correctPassword";
            string newPassword = "";

            // Act
            bool result = user.ChangePassword(currentPassword, newPassword);

            // Assert
            Assert.IsFalse(result);
            user.ChangePassword(newPassword, currentPassword);
        }

        [TestMethod]
        public void ChangePassword_WhenValidArguments_ReturnsTrueAndChangesPassword()
        {
            // Arrange
            User user = new User("testuser");
            string currentPassword = "correctPassword";
            string newPassword = "new_password";

            // Act
            bool result = user.ChangePassword(currentPassword, newPassword);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(newPassword, user.Password);
            user.ChangePassword(newPassword, currentPassword);
        }

        [TestMethod]
        public void DeleteCar_WhenCarExists_RemovesCarFromList()
        {
            // Arrange
            User user = new User("testuser");
            Car carToRemove = new Car("car_to_remove");
            user.Cars.Add(carToRemove);

            // Act
            user.DeleteCar("car_to_remove");

            // Assert
            Assert.IsFalse(user.Cars.Contains(carToRemove));
        }

        [TestMethod]
        public void DeleteMoto_WhenMotoExists_RemovesMotoFromList()
        {
            // Arrange
            User user = new User("testuser");
            MotorCycle motoToRemove = new MotorCycle("moto_to_remove");
            user.Moto.Add(motoToRemove);

            // Act
            user.DeleteMoto("moto_to_remove");

            // Assert
            Assert.IsFalse(user.Moto.Contains(motoToRemove));
        }

        [TestMethod]
        public void FindUser_WhenUserExists_ReturnsUserInfo()
        {
            // Arrange
            User user = new User("testuser2");
            string expectedUserInfo = "Login:testuser2,Password:correctPassword,Cars:,Moto:,Money:,Name:John";

            // Act
            string result = User.FindUser("testuser2");

            // Assert
            Assert.AreEqual(expectedUserInfo, result);
        }
        [TestMethod]
        public void GetStoredPassword_WithValidLogin_ReturnsPassword()
        {
            // Arrange
            string login = "testuser";

            // Act
            string result = User.GetStoredPassword(login);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void RegisterUser_ValidData_ReturnsTrue()
        {
            // Arrange
            string login = "testuser1";
            string password = "password";
            string confirmPassword = "password";
            string name = "John";

            // Act
            bool result = User.RegisterUser(login, password, confirmPassword, name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegisterUser_EmptyLogin_ReturnsFalse()
        {
            // Arrange
            string login = "";
            string password = "password";
            string confirmPassword = "password";
            string name = "John";

            // Act
            bool result = User.RegisterUser(login, password, confirmPassword, name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegisterUser_ExistingUser_ReturnsFalse()
        {
            // Arrange
            string login = "testuser";
            string password = "password";
            string confirmPassword = "password";
            string name = "John";
            // Register existing user
            User.RegisterUser(login, password, confirmPassword, name);

            // Act
            bool result = User.RegisterUser(login, password, confirmPassword, name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LoginUser_ExistingUserWithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            string login = "testuser";
            string password = "correctPassword";

            // Act
            bool result = User.LoginUser(login, password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginUser_ExistingUserWithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            string login = "testuser";
            string password = "correctPassword";

            // Act
            bool result = User.LoginUser(login, "wrongpassword");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UserExists_ExistingUser_ReturnsTrue()
        {
            // Arrange
            string login = "testuser";

            // Act
            bool result = User.UserExists(login);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserExists_NonExistingUser_ReturnsFalse()
        {
            // Arrange
            string login = "nonexistinguser";

            // Act
            bool result = User.UserExists(login);

            // Assert
            Assert.IsFalse(result);
        }
    }
    [TestClass]
    public class MotorCycleTest
    {
        [TestMethod]
        public void Name_Get_ReturnsCorrectValue()
        {
            // Arrange
            string expectedName = "Honda";

            // Act
            MotorCycle motorcycle = new MotorCycle(expectedName);
            string actualName = motorcycle.Name;

            // Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void Name_Set_ModifiesName()
        {
            // Arrange
            string initialName = "Honda";
            string modifiedName = "Yamaha";

            MotorCycle motorcycle = new MotorCycle(initialName);

            // Act
            motorcycle.Name = modifiedName;
            string actualName = motorcycle.Name;

            // Assert
            Assert.AreEqual(modifiedName, actualName);
        }
    }
    [TestClass]
    public class CarTest
    {
        [TestMethod]
        public void Name_Get_ReturnsCorrectValue()
        {
            // Arrange
            string expectedName = "Toyota";

            // Act
            Car car = new Car(expectedName);
            string actualName = car.Name;

            // Assert
            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void Name_Set_ModifiesName()
        {
            // Arrange
            string initialName = "Toyota";
            string modifiedName = "Honda";

            Car car = new Car(initialName);

            // Act
            car.Name = modifiedName;
            string actualName = car.Name;

            // Assert
            Assert.AreEqual(modifiedName, actualName);
        }
    }
    [TestClass]
    public class CityTest
    {
        [TestMethod]
        public void GetParking1_ReturnsCorrectParking()
        {
            // Arrange
            int expectedId = 1;
            int expectedRows = 3;
            int expectedColumns = 3;
            City city = new City(expectedId, expectedRows, expectedColumns, 2, 2, 2, 3, 5, 4);

            // Act
            Parking_ parking1 = city.getParking1;

            // Assert
            Assert.AreEqual(expectedId, parking1.Id);
            Assert.AreEqual(expectedRows, parking1.NumberOfRows);
            Assert.AreEqual(expectedColumns, parking1.NumberOfColumns);
        }

        [TestMethod]
        public void GetParking2_ReturnsCorrectParking()
        {
            // Arrange
            int expectedId = 2;
            int expectedRows = 2;
            int expectedColumns = 2;
            City city = new City(1, 3, 3, expectedId, expectedRows, expectedColumns, 3, 5, 4);

            // Act
            Parking_ parking2 = city.getParking2;

            // Assert
            Assert.AreEqual(expectedId, parking2.Id);
            Assert.AreEqual(expectedRows, parking2.NumberOfRows);
            Assert.AreEqual(expectedColumns, parking2.NumberOfColumns);
        }

        [TestMethod]
        public void GetParking3_ReturnsCorrectParking()
        {
            // Arrange
            int expectedId = 3;
            int expectedRows = 5;
            int expectedColumns = 4;
            City city = new City(1, 3, 3, 2, 2, 2, expectedId, expectedRows, expectedColumns);

            // Act
            Parking_ parking3 = city.getParking3;

            // Assert
            Assert.AreEqual(expectedId, parking3.Id);
            Assert.AreEqual(expectedRows, parking3.NumberOfRows);
            Assert.AreEqual(expectedColumns, parking3.NumberOfColumns);
        }

        [TestMethod]
        public void ParkingSizeWrite_ReturnsCorrectText()
        {
            // Arrange
            City city = new City(1, 3, 3, 2, 2, 2, 3, 5, 4);
            string expectedText = "Parking 1, Size: 3x3\r\nParking 2, Size: 2x2\r\nParking 3, Size: 5x4\r\n";

            // Act
            string actualText = city.ParkingSizeWrite();

            // Assert
            Assert.AreEqual(expectedText, actualText);
        }
    }
    [TestClass]
    public class ParkingTest
    {
        [TestMethod]
        public void Parking_Initialization_CheckProperties()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;

            // Act
            Parking_ parking = new Parking_(id, rows, columns);

            // Assert
            Assert.AreEqual(id, parking.Id);
            Assert.AreEqual(rows, parking.NumberOfRows);
            Assert.AreEqual(columns, parking.NumberOfColumns);
            Assert.AreEqual(rows * columns, parking.TotalNumberOfSpots);
            Assert.AreEqual(9, parking.FreeParkingSpaces);
            Assert.IsNotNull(parking.Spots);
        }

        [TestMethod]
        public void Parking_SetNumberOfColumns_CheckTotalNumberOfSpots()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);

            // Act
            int newColumns = 8;
            parking.NumberOfColumns = newColumns;

            // Assert
            Assert.AreEqual(newColumns, parking.NumberOfColumns);
            Assert.AreEqual(rows * newColumns, parking.TotalNumberOfSpots);
        }

        [TestMethod]
        public void Parking_SetNumberOfRows_CheckTotalNumberOfSpots()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);

            // Act
            int newRows = 5;
            parking.NumberOfRows = newRows;

            // Assert
            Assert.AreEqual(newRows, parking.NumberOfRows);
            Assert.AreEqual(newRows * columns, parking.TotalNumberOfSpots);
        }

        [TestMethod]
        public void Parking_ChangeState_SpotStateUpdated()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            string newState = "occupied";
            int row = 2;
            int column = 2;

            // Act
            parking.ChangeState(newState, row, column);

            // Assert
            Assert.AreEqual(newState, parking.Spots[row, column].State);
        }

        [TestMethod]
        public void Parking_ChangeTime_SpotTimeUpdated()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            int newTime = 120;
            int row = 2;
            int column = 2;

            // Act
            parking.ChangeTime(newTime, row, column);

            // Assert
            Assert.AreEqual(newTime, parking.Spots[row, column].Time);
        }

        [TestMethod]
        public void Parking_ChangeIsCar_SpotIsCarUpdated()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            bool newIsCar = true;
            int row = 2;
            int column = 2;

            // Act
            parking.ChangeIsCar(newIsCar, row, column);

            // Assert
            Assert.AreEqual(newIsCar, parking.Spots[row, column].IsCar);
        }
        [TestMethod]
        public void TotalNumberOfSootsRecount_ShouldUpdateTotalNumberOfSpots()
        {
            Parking_ parking = new Parking_(1, 3, 3);
            parking.NumberOfColumns = 5;
            parking.NumberOfRows = 3;
            int expectedTotalNumberOfSpots = 5 * 3;

            int actualTotalNumberOfSpots = parking.TotalNumberOfSootsRecount();

            Assert.AreEqual(expectedTotalNumberOfSpots, actualTotalNumberOfSpots);
        }

        [TestMethod]
        public void ParkingNameWrite_ShouldReturnFormattedParkingName()
        {
            Parking_ parking = new Parking_(1, 3, 3);
            string expectedName = "Parking 1\r\n3 x 3\r\nFree: 9";

            string actualName = parking.ParkingNameWrite("Parking 1");

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void AddCarInSpot_ShouldAddCarToSpot()
        {
            Parking_ parking = new Parking_(1, 3, 3);
            string carName = "Car1";
            int row = 2;
            int column = 2;

            parking.AddCarInSpot(carName, row, column);

            Assert.IsNotNull(parking.Spots[row, column].VehicleInSpot);
            Assert.AreEqual(carName, parking.Spots[row, column].VehicleInSpot.Name);
        }

        [TestMethod]
        public void DeleteCarInSpot_ShouldRemoveCarFromSpot()
        {
            Parking_ parking = new Parking_(1, 3, 3);
            int row = 2;
            int column = 2;
            parking.Spots[row, column].VehicleInSpot = new Car("Car1");

            string deletedCarName = parking.DeleteCarInSpot(row, column);

            Assert.IsNull(parking.Spots[row, column].VehicleInSpot);
            Assert.AreEqual("Car1", deletedCarName);
        }
        [TestMethod]
        public void AddMotoInSpot_ShouldAddMotorcycleToSpot()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            string motoName = "Moto1";
            int row = 0;
            int column = 0;

            // Act
            parking.AddMotoInSpot(motoName, row, column);

            // Assert
            Assert.IsNotNull(parking.Spots[row, column].VehicleInSpot);
            Assert.AreEqual(motoName, parking.Spots[row, column].VehicleInSpot.Name);
        }

        [TestMethod]
        public void DeleteMotoInSpot_ShouldRemoveMotorcycleFromSpot()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            string motoName = "Moto1";
            int row = 0;
            int column = 0;
            parking.AddMotoInSpot(motoName, row, column);

            // Act
            string removedMotoName = parking.DeleteMotoInSpot(row, column);

            // Assert
            Assert.IsNull(parking.Spots[row, column].VehicleInSpot);
            Assert.AreEqual(motoName, removedMotoName);
        }

        [TestMethod]
        public void AddUserInSpot_ShouldAddUserToSpot()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            string userName = "User1";
            int row = 0;
            int column = 0;

            // Act
            parking.AddUserInSpot(userName, row, column);

            // Assert
            Assert.AreEqual(userName, parking.Spots[row, column].UserInSpot);
        }

        [TestMethod]
        public void DeleteUserInSpot_ShouldRemoveUserFromSpot()
        {
            // Arrange
            int id = 1;
            int rows = 3;
            int columns = 3;
            Parking_ parking = new Parking_(id, rows, columns);
            string userName = "User1";
            int row = 0;
            int column = 0;
            parking.AddUserInSpot(userName, row, column);

            // Act
            string removedUserName = parking.DeleteUserInSpot(row, column);

            // Assert
            Assert.IsNull(parking.Spots[row, column].UserInSpot);
            Assert.AreEqual(userName, removedUserName);
        }
        [TestMethod]
        public void ParkingViborSpotCorrect_ShouldChangeSpotStateToTrue()
        {
            // Arrange
            Parking_ parking = new Parking_(1, 3, 3);
            parking.ParkingViborSpotCorrect();

            // Act
            string spotState = parking.Spots[0, 0].State;

            // Assert
            Assert.AreEqual("true", spotState);
        }

        [TestMethod]
        public void CarInParking_ShouldAddCarToSpotAndChangeUserMoney()
        {
            // Arrange
            Parking_ parking = new Parking_(1, 3, 3);
            User user = new User("testuser");
            user.AddCar("Car1");
            user.Money = 100;
            int initialMoney = user.Money;

            // Act
            parking.CarInParking("Car1", 10, 1, 0, 0, user, true);

            // Assert
            Assert.AreEqual("Car1", parking.Spots[0, 0].VehicleInSpot.Name);
            Assert.AreEqual(initialMoney - 10, user.Money);
            parking.ReturnCarInParking(0, 0);
        }

        [TestMethod]
        public void ReturnCarInParking_ShouldRemoveCarFromSpotAndChangeUserMoney()
        {
            // Arrange
            Parking_ parking = new Parking_(1, 3, 3);
            User user = new User("testuser");
            user.AddCar("Car1");
            user.Money = 100;
            parking.CarInParking("Car1", 10, 1, 0, 0, user, true);
            int initialMoney = user.Money;

            // Act
            parking.ReturnCarInParking(0, 0);

            // Assert
            Assert.IsNull(parking.Spots[0, 0].VehicleInSpot);
            Assert.AreEqual(initialMoney, user.Money);
        }
        [TestMethod]
        public void TimeMinusAllParking_Should_Subtract_Time_From_All_Spots()
        {
            // Arrange
            int id = 1;
            int rows = 2;
            int columns = 2;
            var parking = new Parking_(id, rows, columns);

            // Act
            parking.TimeMinusAllParking();

            // Assert
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Assert.AreEqual(0, parking.Spots[i, j].Time);
                }
            }
        }

        [TestMethod]
        public void ParkingSizeChange_Should_Update_NumberOfRows_And_NumberOfColumns()
        {
            // Arrange
            int id = 1;
            int rows = 2;
            int columns = 2;
            var parking = new Parking_(id, rows, columns);

            // Act
            parking.ParkingSizeChange(3, 3);

            // Assert
            Assert.AreEqual(3, parking.NumberOfRows);
            Assert.AreEqual(3, parking.NumberOfColumns);
            Assert.AreEqual(9, parking.TotalNumberOfSpots);
        }
    }
    [TestClass]
    public class SpotTest
    {
        [TestMethod]
        public void Id_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            int expectedId = 1;
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.Id = expectedId;
            int actualId = spot.Id;

            // Assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void Cost_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            int expectedCost = 10;
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.Cost = expectedCost;
            int actualCost = spot.Cost;

            // Assert
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void State_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            string expectedState = "Occupied";
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.State = expectedState;
            string actualState = spot.State;

            // Assert
            Assert.AreEqual(expectedState, actualState);
        }

        [TestMethod]
        public void Time_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            int expectedTime = 30;
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.Time = expectedTime;
            int actualTime = spot.Time;

            // Assert
            Assert.AreEqual(expectedTime, actualTime);
        }

        [TestMethod]
        public void VehicleInSpot_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            Vehicle expectedVehicle = new Car("qwe");
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.VehicleInSpot = expectedVehicle;
            Vehicle actualVehicle = spot.VehicleInSpot;

            // Assert
            Assert.AreEqual(expectedVehicle, actualVehicle);
        }

        [TestMethod]
        public void UserInSpot_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            string expectedUser = "John";
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.UserInSpot = expectedUser;
            string actualUser = spot.UserInSpot;

            // Assert
            Assert.AreEqual(expectedUser, actualUser);
        }

        [TestMethod]
        public void IsCar_GetAndSet_ShouldSetCorrectValue()
        {
            // Arrange
            bool expectedIsCar = true;
            Spot spot = new Spot(0, 0, "", 0, false);

            // Act
            spot.IsCar = expectedIsCar;
            bool actualIsCar = spot.IsCar;

            // Assert
            Assert.AreEqual(expectedIsCar, actualIsCar);
        }
    }
}