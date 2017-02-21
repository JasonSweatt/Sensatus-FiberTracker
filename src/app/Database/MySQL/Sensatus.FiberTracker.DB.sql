--
-- Definition of table `expense_details`
--
CREATE TABLE `expense_details` (
  `Exp_Id` int(11) NOT NULL auto_increment,
  `Item_Id` int(11) default '0',
  `Exp_Desc` varchar(255) default NULL,
  `Exp_Amount` int(11) default '0',
  `Exp_By` int(11) default '0',
  `Exp_Date` datetime default NULL,
  `MonthYear` varchar(50) default NULL,
  `Finalized` int(11) default '0',
  `IsDeleted` int(11) default '0',
  PRIMARY KEY  (`Exp_Id`),
  KEY `Item_Id` (`Item_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Definition of table `item_details`
--
DROP TABLE IF EXISTS `item_details`;
CREATE TABLE `item_details` (
  `Item_Id` int(11) NOT NULL auto_increment,
  `Item_Name` varchar(50) default NULL,
  `Item_Desc` varchar(250) default NULL,
  `Created_By` varchar(50) default NULL,
  `Entry_Date` datetime default NULL,
  `IsActive` int(11) default '0',
  PRIMARY KEY  (`Item_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table 'item_Details'
--
INSERT INTO item_Details (Item_Name, Item_Desc, Created_By, IsActive)
VALUES('TestItem','Please edit this item details. Its simply added for test purpose.',1,1);


--
-- Definition of table `roledetails`
--

DROP TABLE IF EXISTS `roledetails`;
CREATE TABLE `roledetails` (
  `RoleId` int(11) NOT NULL auto_increment,
  `Role` varchar(255) default NULL,
  `Description` varchar(255) default NULL,
  PRIMARY KEY  (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roledetails`
--

INSERT INTO `roledetails` (`Role`,`Description`) VALUES 
 ('Admin','Super User'),
 ('User',NULL);



--
-- Definition of table `user_info`
--
CREATE TABLE `user_info` (
  `User_Id` int(11) NOT NULL auto_increment,
  `RoleId` int(11) default NULL,
  `User_Name` varchar(50) default NULL,
  `Pwd` varchar(50) default NULL,
  `First_Name` varchar(50) default NULL,
  `Last_Name` varchar(50) default NULL,
  `EMail` varchar(255) default NULL,
  `Mobile` varchar(255) default NULL,
  `Last_Login_Date` datetime default NULL,
  `Password_Change_Date` datetime default NULL,
  `IsActive` int(11) default '0',
  PRIMARY KEY  (`User_Id`),
  KEY `RoleId` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_info`
--
INSERT INTO `user_info` (`RoleId`,`User_Name`,`Pwd`,`First_Name`,`Last_Name`,`EMail`,`Mobile`,`Last_Login_Date`,`Password_Change_Date`,`IsActive`) VALUES 
 (1,'admin','a6jr0tclfyWJWKaPi9URIQ==','Admin','Admin','ak.tripathi@yahoo.com','9880946821','2009-08-24 00:00:00','2009-08-18 00:00:00',1);
