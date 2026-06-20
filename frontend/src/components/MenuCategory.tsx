import "./MenuCategory.css";
import MenuItem from "./MenuItem";

type Item = {
  name: string;
  description: string;
  price: string;
};

type MenuCategoryProps = {
  title: string;
  items: Item[];
};

function MenuCategory({ title, items }: MenuCategoryProps) {
  return (
    <section className="menu-category">
      <h2>{title}</h2>

      <div className="menu-category-card">
        {items.map((item) => (
          <MenuItem
            key={item.name}
            name={item.name}
            description={item.description}
            price={item.price}
          />
        ))}
      </div>
    </section>
  );
}

export default MenuCategory;
