# Ejercicios-NET — Sistema de Operaciones Bancarias

Sistema bancario para gestion de cuentas, depositos, retiros, transferencias y consulta de saldos.

**Stack:** ASP.NET Core 9 (API + MVC) + SQL Server (Docker) + jQuery/Ajax + Procedimientos Almacenados

---

## Estructura del Proyecto

```
Ejercicios-NET/
├── docker-compose.yml              # SQL Server en Docker
├── sql/init.sql                    # BD + procedimientos almacenados
├── .env.example                    # Variables de entorno (copiar a .env)
├── backend/
│   └── BancoApp.Backend/           # API REST (.NET 9)
│       ├── Controllers/            # API Controllers
│       ├── Services/               # Logica de negocio (interfaces + impl)
│       ├── Data/                   # Acceso a datos (ADO.NET, sin EF)
│       ├── Models/                 # Entidades del dominio
│       └── Routes/                 # Configuracion de rutas
└── frontend/
    └── BancoApp.Frontend/          # MVC Frontend (.NET 9)
        ├── Controllers/            # MVC Controllers (thin)
        ├── Views/                  # Vistas Razor + jQuery/Ajax
        └── Models/                 # View models
```

---

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

---

## Levantar el Proyecto

### 1. Configurar variables de entorno

```bash
cp .env.example .env
```

### 2. Levantar SQL Server en Docker

```bash
docker-compose up -d
```

Espera ~30 segundos a que SQL Server este listo.

### 3. Levantar el Backend (API)

```bash
cd backend/BancoApp.Backend
dotnet run
```

API disponible en: **http://localhost:5000**
Swagger UI: **http://localhost:5000/swagger**

### 4. Levantar el Frontend (MVC)

En otra terminal:

```bash
cd frontend/BancoApp.Frontend
dotnet run
```

Frontend disponible en: **http://localhost:5128**

---

## API Endpoints

| Metodo | Endpoint | Descripcion |
|--------|----------|-------------|
| GET | `/api/cuentas` | Listar todas las cuentas |
| GET | `/api/cuentas/saldos` | Consultar saldos |
| GET | `/api/cuentas/{nroCuenta}` | Obtener cuenta por numero |
| POST | `/api/cuentas` | Crear cuenta |
| GET | `/api/cuentas/monedas` | Listar monedas |
| POST | `/api/movimientos/abono` | Registrar deposito |
| POST | `/api/movimientos/retiro` | Registrar retiro |
| POST | `/api/movimientos/transferencia` | Realizar transferencia |
| GET | `/api/movimientos/{nroCuenta}` | Consultar movimientos |

---

## Funcionalidades

| Pantalla | Descripcion |
|----------|-------------|
| **Adicion de Cuentas** | Crear cuentas de ahorro (14 digitos) o corriente (13 digitos) en BOL o USD |
| **Abonos / Retiros** | Depositar o retirar fondos de cuentas registradas |
| **Transferencias** | Transferir fondos entre cuentas |
| **Consulta de Saldos** | Ver saldos de todas las cuentas con boton para ver movimientos |
| **Movimientos** | Historial de movimientos de una cuenta seleccionada |

---

## Arquitectura por Capas

### Backend (API)

```
Controllers  →  Services  →  Data  →  SQL Server
   ↓              ↓           ↓
 HTTP Request   Logica de    ADO.NET
                Negocio      (Procedimientos
                             Almacenados)
```

- **Controllers**: Manejan requests HTTP, validan input, llaman a Services
- **Services**: Logica de negocio, validaciones, orquestacion
- **Data**: Acceso a datos con ADO.NET/SqlClient, ejecuta SPs
- **Models**: Entidades del dominio compartidas

### Frontend (MVC)

```
Views  →  Controllers  →  Backend API (via Ajax)
   ↓
jQuery/Ajax
```

- **Controllers**: Thin controllers, solo sirven vistas
- **Views**: Razor + jQuery/Ajax para comunicarse con el API

---

## Base de Datos

- **Server:** `localhost,1433`
- **User:** `sa`
- **Password:** `Banco2026!`
- **Database:** `BancoDB`

### Procedimientos Almacenados

- `SP_CREAR_CUENTA` — Crear cuenta con validaciones
- `SP_LISTAR_CUENTAS` — Listar todas las cuentas
- `SP_CONSULTAR_SALDOS` — Consulta de saldos
- `SP_OBTENER_CUENTA` — Obtener cuenta por numero
- `SP_REGISTRAR_ABONO` — Registrar deposito
- `SP_REGISTRAR_RETIRO` — Registrar retiro (valida saldo)
- `SP_REALIZAR_TRANSFERENCIA` — Transferencia entre cuentas (valida saldo)
- `SP_CONSULTAR_MOVIMIENTOS` — Historial de movimientos

---

## Detener

```bash
docker-compose down
```

Para eliminar los datos: `docker-compose down -v`
