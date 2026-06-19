/* ========================================
   IMPORTS
======================================== */

import "./SnackProductCard.css";

/* ========================================
   PROPS
======================================== */

type SnackProductCardProps = {
  image: string;
  title: string;
  price: number;
  quantity: number;
  onAdd: () => void;
  onRemove: () => void;
};

/* ========================================
   COMPONENT
======================================== */

function SnackProductCard({
  image,
  title,
  price,
  quantity,
  onAdd,
  onRemove,
}: SnackProductCardProps) {
  return (
    <article className="snack-product-card">
      {/* ========================================
          PRODUCT AFBEELDING
      ======================================== */}

      <img
        src={image}
        alt={title}
        className="snack-product-image"
      />

      {/* ========================================
          PRODUCT TITEL
      ======================================== */}

      <h3>{title}</h3>

      {/* ========================================
          PRIJS + ACTIES
      ======================================== */}

      <div className="snack-product-bottom">
        <span className="snack-product-price">
          € {price.toFixed(2).replace(".", ",")}
        </span>

        {/* ========================================
            KNOPPEN
        ======================================== */}

        {quantity === 0 ? (
          <button
            className="snack-product-button"
            onClick={onAdd}
            aria-label={`Voeg ${title} toe`}
          >
            +
          </button>
        ) : (
          <div className="snack-product-actions">
            <button
              className="snack-product-button"
              onClick={onRemove}
              aria-label={`Verwijder ${title}`}
            >
              −
            </button>

            <span className="snack-product-quantity">
              {quantity}
            </span>

            <button
              className="snack-product-button"
              onClick={onAdd}
              aria-label={`Voeg ${title} toe`}
            >
              +
            </button>
          </div>
        )}
      </div>
    </article>
  );
}

export default SnackProductCard;