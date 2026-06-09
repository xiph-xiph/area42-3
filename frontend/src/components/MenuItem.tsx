import "./MenuItem.css";

type MenuItemProps = {
  name: string;
  description: string;
  price: string;
};

function MenuItem({
  name,
  description,
  price,
}: MenuItemProps) {
  return (
    <article className="menu-item">
      <div className="menu-item-header">
        <h3>{name}</h3>
        <span>{price}</span>
      </div>

      <p>{description}</p>
    </article>
  );
}

export default MenuItem;