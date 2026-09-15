<div align="center">

<img src="docs/shopilo-readme-logo.png" alt="Shopilo — premium fashion and lifestyle ecommerce" width="760" />

# Shopilo — شوبيلو

### Curated style. Effortless shopping.

### أناقة مختارة. تجربة تسوّق بلا تعقيد.

A polished full-stack ecommerce portfolio project built around a complete shopping journey—from product discovery to persistent order placement.

</div>

---

## The Shopilo Experience

Online shopping should feel inviting before it feels functional. Shopilo brings the rhythm of a premium fashion store to the browser: editorial campaigns draw people in, focused discovery tools help shoppers find the right product, and a calm cart experience keeps every buying decision clear.

Shopilo is more than a product grid. Shoppers can search the catalog, narrow results by category and price, compare products through ratings and reviews, save favourites, manage quantities, unlock free shipping, apply promotional codes, and place an order through one consistent experience.

The interface keeps its distinctive visual identity across desktop and mobile while a database-backed application preserves products, carts, inventory, and orders behind the scenes.

---

## The Storefront

### Editorial Home Experience

![Shopilo home experience](screenshots/home-hero-hd.png)

A responsive split-layout hero introduces seasonal collections through bold typography, campaign photography, navigation shortcuts, animated slides, and clear calls to action.

### Curated Product Discovery

![Featured products](screenshots/featured-products-hd.png)

Featured products, collection campaigns, category discovery, promotional messaging, and service guarantees create a convincing retail journey beyond a standard product grid.

---

## Shopping Experience

### Searchable Product Catalog

![Product catalog](screenshots/product-catalog-hd.png)

Shoppers can explore the catalog with debounced search, category filters, price ranges, multiple sorting modes, shareable URL parameters, and client-side pagination.

### Detailed Product Pages

![Product detail page](screenshots/product-detail-hd.png)

Each product page combines imagery, pricing and discounts, stock status, quantity controls, tags, customer reviews, related recommendations, recently viewed products, and direct cart or wishlist actions.

### Persistent Wishlist

![Wishlist](screenshots/wishlist-hd.png)

Saved items remain available across browser sessions through a dedicated frontend state layer synchronized with browser storage.

### Database-Backed Shopping Cart

![Shopping cart](screenshots/shopping-cart-hd.png)

The cart supports quantity controls, item removal, server-validated stock, dynamic totals, free-shipping thresholds, coupon validation, and persistent guest order placement.

---

## What the Project Demonstrates

- A complete responsive shopping journey without changing the original Shopilo visual design
- Search, category filtering, price filtering, sorting, and product details
- Persistent wishlist, recently viewed products, cart, inventory, and orders
- Server-side coupon, shipping, stock, and order-total rules
- Clear loading states, empty states, validation feedback, and error handling
- A focused fresh-graduate codebase with visible responsibilities and practical architecture

---

## Technical Implementation

[![React](https://img.shields.io/badge/React-18-61DAFB?style=flat-square&logo=react&logoColor=white)](https://react.dev/)
[![Vite](https://img.shields.io/badge/Vite-8-646CFF?style=flat-square&logo=vite&logoColor=white)](https://vite.dev/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-3-06B6D4?style=flat-square&logo=tailwindcss&logoColor=white)](https://tailwindcss.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![EF Core](https://img.shields.io/badge/EF_Core-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)


### Technology

| Area | Technology and purpose |
|---|---|
| Frontend | React 18 single-page application |
| Build tool | Vite 8 development server and production build |
| Routing | React Router 7 storefront navigation |
| Styling | Tailwind CSS 3 and custom responsive styles |
| Backend | ASP.NET Core 10 Web API |
| Data access | Entity Framework Core 10 with LINQ |
| Database | SQL Server LocalDB for local development |
| External integration | DummyJSON catalog import for development seed data |
| Testing | xUnit service-level business-rule tests |

### Project Structure

```text
Shopilo-Ecommerce-FullStack/
├── backend/
│   ├── ShopiloApi/
│   │   ├── Controllers/       # HTTP endpoints
│   │   ├── Services/          # Business rules and orchestration
│   │   ├── Repositories/      # EF Core data access
│   │   ├── Interfaces/        # Service and repository contracts
│   │   ├── Models/            # Database entities
│   │   ├── DTOs/              # API request and response contracts
│   │   ├── Data/              # EF Core DbContext
│   │   ├── Middleware/        # Consistent exception responses
│   │   └── Migrations/        # Database schema history
│   └── ShopiloApi.Tests/      # Automated backend tests
├── src/
│   ├── components/            # Shared React components
│   ├── context/               # Frontend state providers
│   ├── hooks/                 # API queries and debounced search
│   ├── pages/                 # Storefront pages
│   └── utils/                 # Shared formatting and helpers
├── docs/                      # README branding assets
└── screenshots/               # Storefront previews
```

---

## Running Locally

### Prerequisites

- .NET 10 SDK
- SQL Server LocalDB
- Node.js 20 or newer
- npm

### 1. Clone the repository

```bash
git clone https://github.com/Abdallah-Sabha1/Shopilo-Ecommerce-Frontend.git
cd Shopilo-Ecommerce-Frontend
```

### 2. Create the local database

```bash
dotnet ef database update --project backend/ShopiloApi
```

### 3. Start the backend

```bash
dotnet run --project backend/ShopiloApi --launch-profile http
```

The API runs at `http://localhost:5183`.

### 4. Import the development catalog once

```http
POST http://localhost:5183/api/seed
```

The import downloads public sample products from DummyJSON and stores them in the local SQL Server database. It requires no API key.

### 5. Start the frontend

```bash
npm install
npm run dev
```

Open `http://localhost:5173`. The frontend calls the backend at `http://localhost:5183/api`.

### Verify the project

```bash
dotnet test backend/ShopiloApi.slnx
npm run build
```



<div align="center">

Built by [**Abdallah Sabha**](https://github.com/Abdallah-Sabha1)

</div>
