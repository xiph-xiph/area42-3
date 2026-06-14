import "./SnackMenuPage.css";

import { useRef, useState } from "react";

import SnackProductCard from "../components/SnackProductCard";
import { snackProducts } from "../data/snackProducts";

// menupagina

function SnackMenuPage() {
  const [cart, setCart] = useState<Record<number, number>>({});

//  caterogieen
 
  const categories = ["Friet", "Snacks", "Burgers", "Dranken", "Desserts"];

  const sectionRefs = useRef<Record<string, HTMLElement | null>>({});

  const addToCart = (id: number) => {
    setCart((prev) => ({
      ...prev,
      [id]: (prev[id] || 0) + 1,
    }));
  };

//   navbar die meescrolt//
  
  const scrollToCategory = (category: string) => {
    sectionRefs.current[category]?.scrollIntoView({
      behavior: "smooth",
      block: "start",
    });
  };

  
//   totaal items
  const totalItems = Object.values(cart).reduce(
    (total, quantity) => total + quantity,
    0
  );

  
//   totaalprijs
  const totalPrice = snackProducts.reduce((total, product) => {
    return total + (cart[product.id] || 0) * product.price;
  }, 0);

  return (
    <main className="snack-menu-page">
      <div className="snack-menu-container">
        <h1>Menu</h1>

        <p className="menu-subtitle">
          Vers bereid • Snel geserveerd
        </p>

        {/* Categorieën scrollfunctie */}

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

        {/* Productgroepen */}

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
                .filter((product) => product.category === category)
                .map((product) => (
                  <SnackProductCard
                    key={product.id}
                    image={product.image}
                    title={product.title}
                    price={product.price}
                    quantity={cart[product.id] || 0}
                    onAdd={() => addToCart(product.id)}
                  />
                ))}
            </div>
          </section>
        ))}

        {/* Winkelwagen */}

        {totalItems > 0 && (
          <div className="shopping-bar">
            <div className="shopping-bar-info">
              <span>{totalItems} artikelen</span>

              <strong>
                € {totalPrice.toFixed(2).replace(".", ",")}
              </strong>
            </div>

            <button className="shopping-bar-button">
              Bekijk bestelling
            </button>
          </div>
        )}
      </div>
    </main>
  );
}

export default SnackMenuPage;