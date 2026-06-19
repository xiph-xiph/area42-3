import { useEffect, useState } from "react";
import "./MenuPage.css";

import MenuCategory from "../components/MenuCategory";
import { menuCategories } from "../data/menuData";

import BackButton from "../components/BackButton";

//  API functies 

function MenuPage() {
  const [activeCategory, setActiveCategory] = useState(menuCategories[0].id);

  const handleNavigationClick = (categoryId: string) => {
    const element = document.getElementById(categoryId);

    if (!element) return;

    element.scrollIntoView({
      behavior: "smooth",
      block: "start",
    });
  };

  useEffect(() => {
    const sections = document.querySelectorAll("section[id]");

    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            setActiveCategory(entry.target.id);
          }
        });
      },
      {
        threshold: 0.4,
      }
    );

    sections.forEach((section) => observer.observe(section));

    return () => {
      sections.forEach((section) => observer.unobserve(section));
    };
  }, []);

  return (
    <main className="menu-page">
      <div className="menu-container">
     
      <BackButton
  to="/restaurant"
  label="Terug naar restaurant"
/>

        <nav className="menu-navigation">
          {menuCategories.map((category) => (
            <button
              key={category.id}
              className={activeCategory === category.id ? "active" : ""}
              onClick={() => handleNavigationClick(category.id)}
            >
              {category.title}
            </button>
          ))}
        </nav>

        {menuCategories.map((category) => (
          <section
            key={category.id}
            id={category.id}
          >
            <MenuCategory
              title={category.title}
              items={category.items}
            />
          </section>
        ))}
      </div>
    </main>
  );
}

export default MenuPage;