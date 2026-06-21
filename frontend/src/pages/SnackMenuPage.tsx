/* ========================================
   IMPORTS
======================================== */

import "./SnackMenuPage.css";

import { useRef, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { getMenu } from "../services/menuService";
import { getCart, addToCart, removeFromCart } from "../services/orderService";

import SnackProductCard from "../components/SnackProductCard";
import type { OrderItem } from "../types/CartDto";
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
  const [cart, setCart] = useState<OrderItem[]>([]);
  const [refreshCart, setRefreshCart] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    getMenu().then((data) => {
      setMenuItems(data.menu);
    });
  }, []);

  useEffect(() => {
    getCart().then((data) => {
      setCart(data.cart.items);
    });
  }, [refreshCart]);

  /* ========================================
     CONSTANTS
  ======================================== */

  const categories = ["Friet", "Snacks", "Burgers", "Dranken", "Desserts"];

  const sectionRefs = useRef<Record<string, HTMLElement | null>>({});

  /* ========================================
     WINKELWAGEN
  ======================================== */

  const handleAddToCart = async (product: MenuItem) => {
    await addToCart(product.id, 1);
    setRefreshCart((prev) => !prev);
  };

  const handleRemoveFromCart = async (productId: number) => {
    await removeFromCart(productId, 1);
    setRefreshCart((prev) => !prev);
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
                    onAdd={() => handleAddToCart(product)}
                    onRemove={() => handleRemoveFromCart(product.id)}
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
              handleAddToCart(product);
            }
          }}
          onRemove={handleRemoveFromCart}
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
