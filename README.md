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

| Outil | Version minimale |
|-------|-----------------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 5.0 |
| MySQL | 5.7 / 8.x |
| XAMPP / WAMP / MySQL Workbench | _(au choix)_ |

---

## Installation

### 1. Cloner le dépôt

```bash
git clone <url-du-repo>
cd BibliothequeApp
```

### 2. Créer la base de données

Exécuter le script SQL fourni à la racine du projet.

**MySQL Workbench :** `File → Open SQL Script → database.sql → ⚡ Exécuter`

**phpMyAdmin :** onglet **Importer** → sélectionner `database.sql`

**Ligne de commande :**
```bash
mysql -u root -p < database.sql
```

Cela crée la base `LibraryDB` et la table `Books` avec 5 livres de démonstration.

### 3. Configurer la connexion

Ouvrir `Data/BookRepository.cs` et modifier la constante en haut de la classe :

```csharp
private const string ConnectionString =
    "server=localhost;port=3306;user=root;password=TON_MOT_DE_PASSE;database=LibraryDB;CharSet=utf8mb4;";
```

| Paramètre  | Valeur par défaut | À modifier si… |
|------------|-------------------|----------------|
| `server`   | `localhost`       | MySQL sur un autre hôte |
| `port`     | `3306`            | Port personnalisé |
| `user`     | `root`            | Autre utilisateur MySQL |
| `password` | _(vide)_          | Mot de passe défini |

### 4. Lancer l'application

```bash
dotnet run
```

Ou depuis Visual Studio : `F5`.

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

| Champ        | Type SQL        | Obligatoire |
|--------------|-----------------|-------------|
| Id           | INT AUTO_INCREMENT | — (auto) |
| Titre        | VARCHAR(255)    | Oui |
| Auteur       | VARCHAR(255)    | Oui |
| ISBN         | VARCHAR(20)     | Oui (unique) |
| AnneePubli   | INT             | Oui |
| Genre        | VARCHAR(100)    | Non |
| Rayon        | VARCHAR(100)    | Non |
| Etagere      | VARCHAR(50)     | Non |
| Disponible   | TINYINT(1)      | Non (défaut : 1) |

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
