# BibliothequeApp

Application de bureau WinForms (.NET 5) pour la gestion d'une bibliothèque.  
Elle permet d'inventorier des livres dans une base de données MySQL via une interface graphique complète.

---

## Fonctionnalités

- **Ajouter** un livre avec tous ses champs
- **Modifier** un livre en le sélectionnant dans la liste
- **Supprimer** un livre (avec confirmation)
- **Rechercher** par titre, auteur, genre ou ISBN
- **Afficher** l'inventaire complet dans un tableau interactif

---

## Prérequis

Avant de commencer, assurez-vous d'avoir installé :

| Outil | Version minimale | Lien |
|-------|-----------------|------|
| .NET SDK | 5.0 | [Télécharger](https://dotnet.microsoft.com/download) |
| XAMPP (inclut MySQL) | toute version récente | [Télécharger](https://www.apachefriends.org/fr/index.html) |

> Si vous utilisez WAMP ou MySQL Workbench à la place de XAMPP, les étapes MySQL restent identiques.

---

## Premier lancement — étape par étape

### Étape 1 — Cloner le projet

Ouvrez un terminal (cmd, PowerShell ou Git Bash) et exécutez :

```bash
git clone https://github.com/RomainD13122/Biblioth-que.git
cd Biblioth-que
```

---

### Étape 2 — Démarrer MySQL avec XAMPP

L'application a besoin que MySQL soit actif **avant** d'être lancée.

1. Ouvrez le **XAMPP Control Panel** (cherchez-le dans le menu Démarrer)
2. Sur la ligne **MySQL**, cliquez sur le bouton **Start**
3. Attendez que le voyant passe au **vert** et qu'un numéro de port (`3306`) apparaisse

> Sans cette étape, l'application affichera une erreur de connexion au démarrage.

---

### Étape 3 — Créer la base de données

C'est une opération à faire **une seule fois**. Elle crée la base `LibraryDB` et la table `Books`.

**Option A — Via phpMyAdmin (recommandé avec XAMPP)**

1. Ouvrez votre navigateur et allez sur `http://localhost/phpmyadmin`
2. Dans le menu de gauche, cliquez sur **Importer** (onglet du haut)
3. Cliquez sur **Choisir un fichier** et sélectionnez le fichier `database.sql` à la racine du projet
4. Faites défiler en bas et cliquez sur **Exécuter**
5. phpMyAdmin affiche un message vert de confirmation

**Option B — Via la ligne de commande**

```bash
C:/xampp/mysql/bin/mysql.exe -u root < database.sql
```

**Option C — Via MySQL Workbench**

1. `File → Open SQL Script` → sélectionner `database.sql`
2. Cliquer sur l'éclair ⚡ pour exécuter

---

### Étape 4 — Vérifier la chaîne de connexion

Ouvrez le fichier `Data/BookRepository.cs` et vérifiez la ligne suivante (vers le haut du fichier) :

```csharp
private const string ConnectionString =
    "server=localhost;port=3306;user=root;password=;database=LibraryDB;CharSet=utf8mb4;";
```

**Avec XAMPP par défaut**, cette ligne n'a pas besoin d'être modifiée (mot de passe vide, port 3306).

Si votre configuration est différente, adaptez ces paramètres :

| Paramètre  | Valeur par défaut | À modifier si… |
|------------|-------------------|----------------|
| `server`   | `localhost`       | MySQL sur un autre hôte |
| `port`     | `3306`            | Port différent dans XAMPP |
| `user`     | `root`            | Vous avez créé un autre utilisateur |
| `password` | _(vide)_          | Vous avez défini un mot de passe root |

---

### Étape 5 — Lancer l'application

Dans le terminal, depuis le dossier du projet :

```bash
dotnet run
```

Ou depuis **Visual Studio** : ouvrez `BibliothequeApp.csproj` puis appuyez sur `F5`.

L'application s'ouvre et charge automatiquement les 5 livres de démonstration insérés par le script SQL.

---

### Récapitulatif du premier lancement

```
1. Cloner le repo
2. Démarrer MySQL dans XAMPP Control Panel
3. Importer database.sql via phpMyAdmin (une seule fois)
4. Vérifier la chaîne de connexion si besoin
5. dotnet run
```

---

## Structure du projet

```
BibliothequeApp/
├── Models/
│   └── Book.cs              # Modèle de données (propriétés du livre)
├── Data/
│   └── BookRepository.cs    # Accès MySQL : CRUD + recherche
├── UI/                      # (dossier réservé aux futurs UserControls)
├── Form1.cs                 # Logique de l'interface (événements, validation)
├── Form1.Designer.cs        # Mise en page des contrôles WinForms
├── Program.cs               # Point d'entrée
├── database.sql             # Script de création de la base de données
└── BibliothequeApp.csproj   # Projet .NET 5 + MySql.Data 9.6.0
```

---

## Modèle de données

| Champ        | Type SQL           | Obligatoire      |
|--------------|--------------------|------------------|
| Id           | INT AUTO_INCREMENT | — (automatique)  |
| Titre        | VARCHAR(255)       | Oui              |
| Auteur       | VARCHAR(255)       | Oui              |
| ISBN         | VARCHAR(20)        | Oui (unique)     |
| AnneePubli   | INT                | Oui              |
| Genre        | VARCHAR(100)       | Non              |
| Rayon        | VARCHAR(100)       | Non              |
| Etagere      | VARCHAR(50)        | Non              |
| Disponible   | TINYINT(1)         | Non (défaut : 1) |

---

## Architecture

Le projet respecte une **architecture en couches** et les principes **SOLID** :

- **Modèle** (`/Models`) — représentation des données, sans logique métier
- **Accès aux données** (`/Data`) — toutes les requêtes SQL isolées du reste
- **Interface** (`Form1`) — uniquement la présentation et la liaison UI/données

---

## Dépendances NuGet

| Package | Version |
|---------|---------|
| [MySql.Data](https://www.nuget.org/packages/MySql.Data) | 9.6.0 |
