/* ========================================
   IMPORTS
======================================== */

import "./SnackMenuPage.css";

import { useRef, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { getMenu } from "../services/menuService";

import SnackProductCard from "../components/SnackProductCard";
import type { CartItem } from "../types/CartItem";
import ShoppingCart from "../components/ShoppingCart";
import type { MenuItem } from "../types/MenuDto";

/* ========================================
   COMPONENT
======================================== */

function SnackMenuPage() {
  /* ========================================
     STATE
  ======================================== */

  const [menuItems, setMenuItems] = useState<MenuItem[]>([]);
  const [cart, setCart] = useState<CartItem[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    getMenu().then((data) => {
      setMenuItems(data.menu);
    });
  }, []);

  /* ========================================
     CONSTANTS
  ======================================== */

  const categories = ["Friet", "Snacks", "Burgers", "Dranken", "Desserts"];

  const sectionRefs = useRef<Record<string, HTMLElement | null>>({});

  /* ========================================
     WINKELWAGEN
  ======================================== */

  const addToCart = (product: MenuItem) => {
    setCart((current) => {
      const existingItem = current.find((item) => item.id === product.id);

      if (existingItem) {
        return current.map((item) =>
          item.id === product.id
            ? {
                ...item,
                quantity: item.quantity + 1,
              }
            : item,
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
            : item,
        )
        .filter((item) => item.quantity > 0),
    );
  };

  /* ========================================
     BEREKENINGEN
  ======================================== */

  const getQuantity = (productId: number) => {
    return cart.find((item) => item.id === productId)?.quantity ?? 0;
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

        <p className="menu-subtitle">Vers bereid • Snel geserveerd</p>

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
              {menuItems
                .filter((product) => product.category === category)
                .map((product) => (
                  <SnackProductCard
                    key={product.id}
                    image={product.imageUrl}
                    title={product.title}
                    price={product.price}
                    quantity={getQuantity(product.id)}
                    onAdd={() => addToCart(product)}
                    onRemove={() => removeFromCart(product.id)}
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
            const product = menuItems.find((p) => p.id === id);

            if (product) {
              addToCart(product);
            }
          }}
          onRemove={removeFromCart}
          onCheckout={() =>
            navigate("/snackhoek/checkout", {
              state: { cart },
            })
          }
        />
      </div>
    </main>
  );
}

export default SnackMenuPage;
