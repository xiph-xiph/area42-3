import "./SnackhoekPage.css";

import { Link } from "react-router-dom";

import FooterInfo from "../components/FooterInfo";
import SnackProductCard from "../components/SnackProductCard";

import { snackProducts } from "../data/snackProducts";

import heroImage from "../assets/images/snackhoek_hero.png";
import logoImage from "../assets/images/snackhoek_logo.png";
import promoImage from "../assets/images/snackhoek_promo.png";
import instagramLogo from "../assets/images/instagram-logo.png";

function SnackhoekPage() {
  const popularProducts = snackProducts.filter(
    (product) => product.popular
  );

  return (
    <main className="snackhoek-page">
      <div className="snackhoek-container">
        {/* Hero */}

        <section className="snack-hero">

          <img
            src={heroImage}
            alt="Snackhoek"
            className="snack-hero-image"
          />

          <img
            src={logoImage}
            alt="Snackhoek logo"
            className="snack-logo"
          />

        </section>

        {/* Slogan */}

        <p className="snack-slogan">
          “Snel en lekker genieten tijdens je verblijf”
        </p>

        {/* Buttons */}

        <section className="snack-buttons">

          <Link
            to="/snackhoek/menu"
            className="snack-button outline"
          >
            Menu
          </Link>

          <button className="snack-button filled">
            Bestel online
          </button>

        </section>

        {/* Promo */}

        <section className="promo-section">

          <h2>Promo</h2>

          <img
            src={promoImage}
            alt="Promo"
            className="promo-image"
          />

        </section>

        {/* Populair */}

        <section className="popular-section">

          <h2>Populair</h2>

          <div className="popular-grid">

          {popularProducts.map((product) => (
  <SnackProductCard
    key={product.id}
    image={product.image}
    title={product.title}
    price={product.price}
    quantity={0}
    onAdd={() => {}}
    onRemove={() => {}}
  />
))}

          </div>

        </section>

        {/* Footer */}

        <FooterInfo
          businessName="De snackhoek"
          address="Eetplein 125"
          postalCode="4567 EP Veghel"
          phone="0764 - 356738"
          openingDays="maandag t/m zondag"
          openingHours="12.00u. - 22.00u."
          socialLogo={instagramLogo}
        />

      </div>
    </main>
  );
}

export default SnackhoekPage;