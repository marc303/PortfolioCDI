<?php
   require_once 'header.php';
   require_once 'login.php';

   try {
    $pdo = new PDO($attr, $user, $pass, $opts);
  } 
  catch (PDOException $e) {
    throw new PDOException($e->getMessage(), (int)$e-getCode());
  }

   if (isset($_POST['add'])) {

      $query = "SELECT e.id, c.description, e.Evaluation FROM evaluations e INNER JOIN cours c ON e.codeCours = c.id WHERE NOT EXISTS ( SELECT null FROM resultats r WHERE r.id = e.id) ORDER BY e.id";
      $result =$pdo->query($query);

      echo <<<_END
        <h3>Ajoutez une évaluation</h3>
        <form action="index.php" method="post">
        <label for="eval">Sélectionnez une évaluation:</label>
        <select name="eval" id="eval">
      _END;

      while ($row = $result->fetch(PDO::FETCH_BOTH)) {
         
        $id = htmlspecialchars($row['id']);
        $desc = htmlspecialchars($row['description']);
        $eval = htmlspecialchars($row['Evaluation']);

        echo <<<_END
          <option value="$id">$desc - $eval</option>
        _END;
      }

      echo <<<_END
        </select>
        <label for="note">Entrez la note de l'évaluation:</label>
        <input type="number" name="note" id="note">
        <input type="submit" name="addNote" value="Confirmez l'ajout">
        </form>
        <button
          onclick="window.location.href='index.php';"
        >Annuler</button>
      _END;
   }
   elseif (isset($_POST['upEval'])) {
      $evalID = mysql_fix_string($_POST['evalID']);

      $query1 = "SELECT c.description, e.Evaluation, r.note FROM resultats r INNER JOIN evaluations e ON r.id = e.id INNER JOIN cours c ON e.codeCours = c.id WHERE r.id = $evalID";
      $result1 = $pdo->query($query1);

      $row1 = $result1->fetch(PDO::FETCH_BOTH);

      $desc1 = htmlspecialchars($row1['description']);
      $eval1 = htmlspecialchars($row1['Evaluation']);
      $note = htmlspecialchars($row1['note']);
      echo <<<_END
        <h3>Modifiez une évaluation</h3>
        <form action="index.php" method="post">
        <input type="hidden" name="evalStart" value="$evalID">
        <label for="eval">Sélectionnez une évaluation:</label>
        <select name="evalEnd" id="eval">
        <option disabled>$desc1 - $eval1</option>
      _END;

      $query2 = "SELECT e.id, c.description, e.Evaluation FROM evaluations e INNER JOIN cours c ON e.codeCours = c.id WHERE NOT EXISTS ( SELECT null FROM resultats r WHERE r.id = e.id) ORDER BY e.id";
      $result2 =$pdo->query($query2);

      while ($row2 = $result2->fetch(PDO::FETCH_BOTH)) {
         
        $id2 = htmlspecialchars($row2['id']);
        $desc2 = htmlspecialchars($row2['description']);
        $eval2 = htmlspecialchars($row2['Evaluation']);

        echo <<<_END
          <option value="$id2">$desc2 - $eval2</option>
        _END;

        
      }
      echo <<<_END
        </select>
        <label for="note">Entrez la note de l'évaluation:</label>
        <input type="number" name="note" id="note" value="$note">
        <input type="submit" name="upNote" value="Confirmez la modification">
        </form>
        <button
          onclick="window.location.href='update.php';"
        >Annuler</button>
      _END;
   }
   elseif (isset($_POST['delEval'])) {
    $evalID = mysql_fix_string($_POST['evalID']);

    $query = "SELECT c.description, e.Evaluation, r.note FROM resultats r INNER JOIN evaluations e ON r.id = e.id INNER JOIN cours c ON e.codeCours = c.id WHERE r.id = $evalID";
    $result = $pdo->query($query);

    $row = $result->fetch(PDO::FETCH_BOTH);

    $desc = htmlspecialchars($row['description']);
    $eval = htmlspecialchars($row['Evaluation']);
    $note = htmlspecialchars($row['note']);

    echo <<<_END
        <h3>Supprimez une évaluation</h3>
        <form action="index.php" method="post">
        <input type="hidden" name="evalID" value="$evalID">
        <label for="eval">Voulez-vous vraiment supprimer l'évaluation de $note % correspondant à "$eval" du cours $desc ?</label>
        <input type="submit" name="delNote" value="Confirmez la suppression">
        </form>
        <button
          onclick="window.location.href='update.php';"
        >Annuler</button>
    _END;
  }
?>
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Document</title>
  <link rel="stylesheet" href="css/styles.css">
</head>
<body>

</body>
</html>