import "./SnackProductCard.css";

type SnackProductCardProps = {
  image: string;
  title: string;
  price: number;
  onAdd?: () => void;
};

function SnackProductCard({
  image,
  title,
  price,
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

        <button
          className="snack-product-button"
          onClick={onAdd}
          aria-label={`Voeg ${title} toe`}
        >
          +
        </button>
      </div>
    </article>
  );
}

export default SnackProductCard;