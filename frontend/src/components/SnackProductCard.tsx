import "./SnackProductCard.css";

type SnackProductCardProps = {
  image: string;
  title: string;
  price: number;
  quantity?: number;
  onAdd?: () => void;
};

function SnackProductCard({
  image,
  title,
  price,
  quantity = 0,
  onAdd,
}: SnackProductCardProps) {
  return (
    <article className="snack-product-card">
      <img
        src={image}
        alt={title}
        className="snack-product-image"
      />

      <h3>{title}</h3>

      <div className="snack-product-bottom">
        <span className="snack-product-price">
          € {price.toFixed(2).replace(".", ",")}
        </span>

        <div className="snack-product-actions">

          {quantity > 0 && (
            <span className="snack-product-quantity">
              {quantity}
            </span>
          )}

          <button
            className="snack-product-button"
            onClick={onAdd}
            aria-label={`Voeg ${title} toe`}
          >
            +
          </button>

        </div>
      </div>
    </article>
  );
}

export default SnackProductCard;