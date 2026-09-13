<div align="center">

<img src="docs/shopilo-readme-logo.png" alt="Shopilo — premium fashion and lifestyle ecommerce" width="760" />

# Shopilo — شوبيلو

### Curated style. Effortless shopping.

### أناقة مختارة. تجربة تسوّق بلا تعقيد.

[![React](https://img.shields.io/badge/React-18-61DAFB?style=flat-square&logo=react&logoColor=white)](https://react.dev/)
[![Vite](https://img.shields.io/badge/Vite-5-646CFF?style=flat-square&logo=vite&logoColor=white)](https://vite.dev/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-3-06B6D4?style=flat-square&logo=tailwindcss&logoColor=white)](https://tailwindcss.com/)
[![DummyJSON](https://img.shields.io/badge/Product_API-DummyJSON-E8521A?style=flat-square)](https://dummyjson.com/docs/products)
[![Netlify](https://img.shields.io/badge/Deployed_on-Netlify-00C7B7?style=flat-square&logo=netlify&logoColor=white)](https://shopilooo.netlify.app/)

[**Explore the live store →**](https://shopilooo.netlify.app/)

</div>

---

## ASP.NET Core Backend

Shopilo now includes a database-backed ASP.NET Core Web API while preserving the existing storefront design and user journey. The React application reads products and categories from the local API, stores cart changes in SQL Server, and creates persistent orders instead of simulating checkout in the browser.

### Backend capabilities

- Product catalog, product details, search, and category filtering
- DummyJSON import endpoint for development sample data
- Persistent guest carts with add, update, remove, and clear operations
- Server-side stock validation
- Server-side coupon, shipping, and order-total calculations
- Persistent orders with price snapshots and stock reduction
- Consistent Problem Details error responses
- EF Core migrations and SQL Server database constraints
- Automated tests for product and order business rules

### Backend structure

```text
React UI
  -> ASP.NET Core Controller
  -> Service
  -> Specific Repository
  -> EF Core DbContext
  -> SQL Server
```

Repositories are specific to each responsibility (`ProductRepository`, `CategoryRepository`, `CartRepository`, and `OrderRepository`). The project intentionally avoids a generic repository, CQRS, MediatR, microservices, JWT, Docker, and cloud infrastructure so the important junior .NET fundamentals remain visible.

### Run the complete application

Apply the database migrations and start the backend:

```bash
dotnet ef database update --project backend/ShopiloApi
dotnet run --project backend/ShopiloApi --launch-profile http
```

In Development, import the sample catalog once:

```http
POST http://localhost:5183/api/seed
```

Start the unchanged React interface in another terminal:

```bash
npm install
npm run dev
```

The frontend runs at `http://localhost:5173` and calls the API at `http://localhost:5183/api`. Authentication is intentionally outside the current scope because the existing interface has no login or registration screens; the implemented workflow is a guest-cart and guest-checkout portfolio flow.

---

## Overview

Online shopping should feel inviting before it feels functional. Shopilo brings the rhythm of a premium fashion store to the browser: editorial campaigns draw people in, thoughtful discovery tools help them find the right product, and a calm, focused cart experience keeps every buying decision clear.

It is more than a product grid. Customers can search a broad catalog, narrow results by category and price, compare products through ratings and reviews, save favourites, manage quantities, unlock free shipping, apply promotional codes, and complete a polished demonstration checkout flow.

Behind that experience is a deliberately lightweight React architecture powered by live DummyJSON product data. Search stays responsive, shopping state survives page refreshes, and every interface—from the hero carousel to product cards and notifications—is built from scratch without a component library. The result is a storefront that looks distinctive, feels considered, and behaves like a real retail product across desktop and mobile.

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

Saved items remain available across sessions through a dedicated Context API state layer synchronized with browser storage.

### Smart Shopping Cart

![Shopping cart](screenshots/shopping-cart-hd.png)

The cart supports quantity controls, item removal, dynamic totals, free-shipping thresholds, coupon validation, discount calculations, and a simulated order confirmation flow.

---

## Key Capabilities

**For shoppers**

- Browse a responsive catalog powered by the DummyJSON Products API
- Search with debounced requests to avoid unnecessary API traffic
- Filter by category and price, then sort by price, rating, or name
- Open detailed product views with reviews, availability, discounts, and recommendations
- Save products to a persistent wishlist
- Maintain a persistent cart with live quantities, totals, shipping, and discounts
- Revisit recently viewed products without starting the search again
- Receive loading skeletons, toast feedback, empty states, and mobile-friendly navigation

**For the frontend architecture**

- Reusable components for navigation, product cards, skeletons, toasts, and page utilities
- Context API and reducers for predictable cart and wishlist state
- Custom hooks for product queries, category loading, search, and debouncing
- URL-driven catalog state for linkable searches and category views
- LocalStorage synchronization for cart, wishlist, and browsing history
- Responsive design built with Tailwind CSS and custom styling

---

## Technology

| | |
|---|---|
| **Frontend** | React 18 — component-driven single-page application |
| **Build Tool** | Vite 5 — development server and optimized production builds |
| **Routing** | React Router 6 — storefront, catalog, product, cart, and wishlist routes |
| **Styling** | Tailwind CSS 3 — responsive layouts, interactions, and visual system |
| **State** | Context API + `useReducer` — cart and wishlist management |
| **Data** | DummyJSON Products API — catalog, images, ratings, reviews, and categories |
| **Persistence** | LocalStorage — cart, wishlist, and recently viewed products |
| **Deployment** | Netlify — static frontend hosting |

---

## Project Structure

```text
src/
├── components/          # Navbar, footer, product cards, skeletons, toasts
├── context/             # Cart, wishlist, and recently viewed state
├── hooks/               # Product API queries and debounced search
├── pages/               # Home, shop, product detail, cart, and wishlist
├── utils/               # Shared formatting and helper functions
├── App.jsx              # Providers and application routes
└── index.css            # Tailwind layers and global visual styles
```

---

## Running Locally

### Prerequisites

- Node.js 18 or newer
- npm

### Installation

```bash
git clone https://github.com/Abdallah-Sabha1/Shopilo-Ecommerce.git
cd Shopilo-Ecommerce
npm install
npm run dev
```

Open the local URL displayed by Vite. No environment variables or API keys are required.

### Production Build

```bash
npm run build
npm run preview
```

---

## Project Scope

Shopilo is intentionally a frontend-only commerce experience. Product data comes from a public demonstration API, while checkout and order placement are simulated in the browser. A production evolution would add authenticated customer accounts, a commerce backend, inventory and order persistence, and a real payment provider.

---

<div align="center">

Built by [**Abdallah Sabha**](https://github.com/Abdallah-Sabha1)

</div>
