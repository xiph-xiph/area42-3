import "./DishCard.css";

type DishCardProps = {
  image: string;
  title: string;
};

function DishCard({ image, title }: DishCardProps) {
  return (
    <article className="dish-card">
      <img src={image} alt={title} className="dish-card__image" />

      <div className="dish-card__overlay">
        <p>{title}</p>
      </div>
    </article>
  );
}

export default DishCard;