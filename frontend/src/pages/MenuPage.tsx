import "./MenuPage.css";

import MenuCategory from "../components/MenuCategory";

import { menuCategories } from "../data/menudata";

function MenuPage() {
  return (
    <main className="menu-page">
      <div className="menu-container">
        <nav className="menu-navigation">
          {menuCategories.map((category) => (
            <button key={category.id}>
              {category.title}
            </button>
          ))}
        </nav>

        {menuCategories.map((category) => (
          <MenuCategory
            key={category.id}
            title={category.title}
            items={category.items}
          />
        ))}
      </div>
    </main>
  );
}

export default MenuPage;