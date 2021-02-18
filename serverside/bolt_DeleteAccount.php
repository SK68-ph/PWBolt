<?php
    session_start();
    // Check if our global vars exist
    if( !(isset($_POST['id']) &&  isset($_SESSION['guid'])))
    {
     die("empty params Failed");
    }
    // Init Database connection
    require_once 'config/config.php';

    // Init vars that will be used later on.
    $id = trim($_POST["id"]);
    $guid = $_SESSION['guid'];
    $userTable = "tbl_userVault".$guid;

    // Delete record in user's table using the id provided by the user
    $sql = "DELETE FROM {$userTable} WHERE id={$id}";
    if ($stmt = mysqli_prepare($link, $sql)) {
        $stmt->execute();
        $stmt->close();
      }
    mysqli_close($link);
?>
