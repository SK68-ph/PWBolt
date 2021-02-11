<?php
    session_start();
    if( !(isset($_POST['id']) &&  isset($_SESSION['guid'])))
    {
     die("empty params Failed");
    }

    $id = trim($_POST["id"]);
    $guid = $_SESSION['guid'];
    $userTable = "tbl_userVault".$guid;
    require_once 'config/config.php';

    $sql = "DELETE FROM {$userTable} WHERE id={$id}";
    if ($stmt = mysqli_prepare($link, $sql)) {
        $stmt->execute();
        $stmt->close();
      }
    mysqli_close($link);
?>
