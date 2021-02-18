<?php
  // Check if post request is valid and provided
    if( !(isset($_POST['username']) &&  isset($_POST['pwbolt'])))
    {
     die("empty params Failed");
    }
    // Init Database connection
    require_once 'config/config.php';

    // Trim our post request to remove unwanted whitespace
    $username = trim($_POST["username"]);
    $pwbolt = trim($_POST["pwbolt"]);

    // see php documentation about password_verify and password_hash for more info
    $hashedPw = password_hash($pwbolt, PASSWORD_BCRYPT);
    if(password_verify($pwbolt, $hashedPw)) {
      //Generate a random number
      $random = random_int(1,2147483047);
      // it will be then used for both uniqid in user's table and the user's tablename
      $sql = "INSERT INTO tbl_users() VALUES('{$random}', '{$username}','{$hashedPw}')";
      if ($stmt = $link->prepare($sql)) {
          if ($stmt->execute()) {
            echo "Status:OK , Register Done";
            $userTable = "tbl_userVault".$random; // concatenate the defualt suffix and the random number for uniqueness and added security
            $sql2 = "CREATE TABLE IF NOT EXISTS {$userTable}(id int PRIMARY KEY AUTO_INCREMENT,website varchar(100),username varchar(100),password varchar(600))";
            if ($stmt2 = $link->prepare($sql2)) {
              $stmt2->execute();
              $stmt2->close();
            }
          }else {
            echo "Status:ERROR, Username already taken";
          }
          $stmt->close();
        }
      }
    mysqli_close($link);
    //#https://elucidative-designa.000webhostapp.com/PWBOLT_register.php
?>
