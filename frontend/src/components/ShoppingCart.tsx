/* ========================================
   IMPORTS
======================================== */

import "./ShoppingCart.css";
import type { CartItem } from "../types/CartItem";

/* ========================================
   PROPS
======================================== */

type ShoppingCartProps = {
  cart: CartItem[];
  onAdd: (id: number) => void;
  onRemove: (id: number) => void;
  onCheckout: () => void;
};

/* ========================================
   COMPONENT
======================================== */

function ShoppingCart({
  cart,
  onAdd,
  onRemove,
  onCheckout,
}: ShoppingCartProps) {
  /* ========================================
     BEREKENINGEN
  ======================================== */

  const totalItems = cart.reduce((total, item) => total + item.quantity, 0);

  const totalPrice = cart.reduce(
    (total, item) => total + item.quantity * item.price,
    0,
  );

  if (cart.length === 0) {
    return null;
  }

  /* ========================================
     RENDER
  ======================================== */

  return (
    <aside className="shopping-cart">
      <h2 className="shopping-cart-title">🛒 Winkelwagen</h2>

      <div className="shopping-cart-items">
        {cart.map((item) => (
          <div key={item.id} className="shopping-cart-item">
            <div>
              <strong>{item.title}</strong>
              <p>€ {item.price.toFixed(2).replace(".", ",")}</p>
            </div>

            <div className="shopping-cart-actions">
              <button onClick={() => onRemove(item.id)}>−</button>

              <span>{item.quantity}</span>

              <button onClick={() => onAdd(item.id)}>+</button>
            </div>
          </div>
        ))}
      </div>

      <div className="shopping-cart-footer">
        <div className="shopping-cart-total">
          <span>{totalItems} artikelen</span>

          <strong>€ {totalPrice.toFixed(2).replace(".", ",")}</strong>
        </div>

        <button className="shopping-cart-button" onClick={onCheckout}>
          Afrekenen
        </button>
      </div>
    </aside>
  );
}

export default ShoppingCart;
