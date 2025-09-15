# Ecommerce Application

A **.NET-based eCommerce application** with a clean architecture, supporting product management, orders, and JWT authentication.

---

## 🚀 Features

* Product catalog (CRUD)
* Order management
* Authentication & Authorization with JWT
* Database integration using Entity Framework Core
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

### 3. Create a `.env` file

In the project root, create a `.env` file with the following variables:

```env
# Database connection
DB_HOST=
DB_PORT=
DB_NAME=
DB_USERNAME=
DB_PASSWORD=

# JWT settings
JWT_KEY=
JWT_ISSUER=
JWT_AUDIENCE=
```

Fill in values according to your local or production environment.

### 4. Apply database migrations

```bash
dotnet ef database update
```

### 5. Run the project

```bash
dotnet run
```

---

## 📖 API Documentation

Once the application is running, open your browser and go to:

```
http://localhost:5000/scalar/v1
```

(or the port configured in your environment)

This will open the **Scalar API Docs**, where you can explore all available endpoints interactively.

---

## 🧩 Tech Stack

* **.NET (Web API)**
* **Entity Framework Core**
* **Scalar** for API documentation
* **JWT** for authentication
* **SQL Database** (configurable via `.env`)

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

Would you like me to also include **example endpoints** (e.g., `/api/products`, `/api/orders`) in the README so users get a quick idea of what the API supports without opening Scalar?
bb
