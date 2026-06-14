/* ========================================
   IMPORTS
======================================== */

import "./SnackMenuPage.css";

import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";

import SnackProductCard from "../components/SnackProductCard";
import { snackProducts } from "../data/snackProducts";
import type { CartItem } from "../types/CartItem";
import ShoppingCart from "../components/ShoppingCart";

/* ========================================
   COMPONENT
======================================== */

function SnackMenuPage() {
  /* ========================================
     STATE
  ======================================== */

  const [cart, setCart] = useState<CartItem[]>([]);
  const navigate = useNavigate();

  /* ========================================
     CONSTANTS
  ======================================== */

  const categories = [
    "Friet",
    "Snacks",
    "Burgers",
    "Dranken",
    "Desserts",
  ];

  const sectionRefs = useRef<Record<string, HTMLElement | null>>({});

  /* ========================================
     WINKELWAGEN
  ======================================== */

  const addToCart = (product: typeof snackProducts[number]) => {
    setCart((current) => {
      const existingItem = current.find(
        (item) => item.id === product.id
      );

      if (existingItem) {
        return current.map((item) =>
          item.id === product.id
            ? {
                ...item,
                quantity: item.quantity + 1,
              }
            : item
        );
      }

      return [
        ...current,
        {
          id: product.id,
          title: product.title,
          price: product.price,
          quantity: 1,
        },
      ];
    });
  };

  const removeFromCart = (productId: number) => {
    setCart((current) =>
      current
        .map((item) =>
          item.id === productId
            ? {
                ...item,
                quantity: item.quantity - 1,
              }
            : item
        )
        .filter((item) => item.quantity > 0)
    );
  };

  /* ========================================
     BEREKENINGEN
  ======================================== */

  const getQuantity = (productId: number) => {
    return (
      cart.find((item) => item.id === productId)?.quantity ?? 0
    );
  };

 
  /* ========================================
     SCROLL
  ======================================== */

  const scrollToCategory = (category: string) => {
    sectionRefs.current[category]?.scrollIntoView({
      behavior: "smooth",
      block: "start",
    });
  };

  /* ========================================
     RENDER
  ======================================== */

  return (
    <main className="snack-menu-page">
      <div className="snack-menu-container">

        {/* ========================================
            HEADER
        ======================================== */}

        <h1>Menu</h1>

        <p className="menu-subtitle">
          Vers bereid • Snel geserveerd
        </p>

        {/* ========================================
            CATEGORIEËN
        ======================================== */}

        <div className="category-row">
          {categories.map((category) => (
            <button
              key={category}
              className="category-button"
              onClick={() => scrollToCategory(category)}
            >
              {category}
            </button>
          ))}
        </div>

        {/* ========================================
            PRODUCTEN
        ======================================== */}

        {categories.map((category) => (
          <section
            key={category}
            className="menu-category"
            ref={(el) => {
              sectionRefs.current[category] = el;
            }}
          >
            <h2>{category}</h2>

            <div className="menu-grid">
              {snackProducts
                .filter(
                  (product) => product.category === category
                )
                .map((product) => (
                  <SnackProductCard
                    key={product.id}
                    image={product.image}
                    title={product.title}
                    price={product.price}
                    quantity={getQuantity(product.id)}
                    onAdd={() => addToCart(product)}
                    onRemove={() =>
                      removeFromCart(product.id)
                    }
                  />
                ))}
            </div>
          </section>
        ))}

        {/* ========================================
            WINKELWAGEN
        ======================================== */}

<ShoppingCart
  cart={cart}
  onAdd={(id) => {
    const product = snackProducts.find((p) => p.id === id);

    if (product) {
      addToCart(product);
    }
  }}
  onRemove={removeFromCart}
  onCheckout={() => navigate("/snackhoek/checkout")}
/>
      </div>
    </main>
  );
}

export default SnackMenuPage;