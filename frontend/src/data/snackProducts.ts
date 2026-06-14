import hamburgerImage from "../assets/images/snackhoek_hamburger.png";
import frikandelImage from "../assets/images/snackhoek_frikandel.png";
import friesImage from "../assets/images/snackhoek_background_opacity_50.png";

export type SnackProduct = {
  id: number;
  title: string;
  price: number;
  image: string;
  category: string;
  popular: boolean;
};

export const snackProducts: SnackProduct[] = [
  {
    id: 1,
    title: "Friet",
    price: 3.95,
    image: friesImage,
    category: "Friet",
    popular: true,
  },
  {
    id: 2,
    title: "Friet Mayo",
    price: 4.45,
    image: friesImage,
    category: "Friet",
    popular: true,
  },
  {
    id: 3,
    title: "Friet Speciaal",
    price: 4.95,
    image: friesImage,
    category: "Friet",
    popular: false,
  },
  {
    id: 4,
    title: "Frikandel",
    price: 3.25,
    image: frikandelImage,
    category: "Snacks",
    popular: true,
  },
  {
    id: 5,
    title: "Frikandel Speciaal",
    price: 3.95,
    image: frikandelImage,
    category: "Snacks",
    popular: true,
  },
  {
    id: 6,
    title: "Hamburger",
    price: 5.95,
    image: hamburgerImage,
    category: "Burgers",
    popular: true,
  },
  {
    id: 7,
    title: "Coca Cola",
    price: 3.95,
    image: hamburgerImage,
    category: "Dranken",
    popular: true,
  },

  {
    id: 7,
    title: "Sundea ijs",
    price: 4.95,
    image: hamburgerImage,
    category: "Desserts",
    popular: true,
  },
];