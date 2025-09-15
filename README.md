# Ecommerce Application

A **.NET-based eCommerce application** with a clean architecture, supporting product management, orders, and JWT authentication.

---

## 🚀 Features

* Product catalog (CRUD)
* Order management
* Authentication & Authorization with JWT
* Database integration using **PostgreSQL + Entity Framework Core**
* API documentation powered by **Scalar**

---

## 🛠️ Setup

### 1. Clone the repository

```bash
git clone https://github.com/mesh-dell/Ecommerce.git
cd Ecommerce
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Create a PostgreSQL database

Make sure you have PostgreSQL installed and running.

Create a new database for the application:

```sql
CREATE DATABASE ecommerce;
```

### 4. Create a `.env` file

In the project root, create a `.env` file with the following variables:

```env
# Database connection
DB_HOST=localhost
DB_PORT=5432
DB_NAME=ecommerce
DB_USERNAME=your_postgres_username
DB_PASSWORD=your_postgres_password

# JWT settings
JWT_KEY=your_secret_key
JWT_ISSUER=EcommerceApp
JWT_AUDIENCE=EcommerceUsers
```

Replace the values with your PostgreSQL setup.

### 5. Apply database migrations

```bash
dotnet ef database update
```

### 6. Run the project

```bash
dotnet run
```

---

## 📖 API Documentation

Once the application is running, open your browser and go to:

```
http://localhost:5000/scalar/v1
```

(or the port configured in your environment).

This will open the **Scalar API Docs**, where you can explore all available endpoints interactively.

---

## 🧩 Tech Stack

* **.NET (Web API)**
* **Entity Framework Core**
* **PostgreSQL** (relational database)
* **Scalar** for API documentation
* **JWT** for authentication

---

## 🤝 Contributing

1. Fork this repository
2. Create a feature branch
3. Commit your changes
4. Push to your branch
5. Open a Pull Request

---

## 📜 License

MIT License — feel free to use and modify.

---
