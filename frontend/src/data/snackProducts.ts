/* ========================================
   IMPORTS
======================================== */

import type { SnackProduct } from "../types/SnackProduct";

import friesImage from "../assets/images/snackhoek_background_opacity_50.png";
import frikandelImage from "../assets/images/snackhoek_frikandel.png";
import hamburgerImage from "../assets/images/snackhoek_hamburger.png";



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
    title: "Grote Friet",
    price: 5.95,
    image: friesImage,
    category: "Friet",
    popular: false,
  },

  /* ========================================
     SNACKS
  ======================================== */

  {
    id: 5,
    title: "Frikandel",
    price: 3.25,
    image: frikandelImage,
    category: "Snacks",
    popular: true,
  },

  {
    id: 6,
    title: "Frikandel Speciaal",
    price: 3.95,
    image: frikandelImage,
    category: "Snacks",
    popular: true,
  },

  {
    id: 7,
    title: "Kroket",
    price: 3.45,
    image: frikandelImage,
    category: "Snacks",
    popular: false,
  },

  {
    id: 8,
    title: "Kaassoufflé",
    price: 3.75,
    image: frikandelImage,
    category: "Snacks",
    popular: false,
  },

  /* ========================================
     BURGERS
  ======================================== */

  {
    id: 9,
    title: "Hamburger",
    price: 5.95,
    image: hamburgerImage,
    category: "Burgers",
    popular: true,
  },

  {
    id: 10,
    title: "Cheeseburger",
    price: 6.45,
    image: hamburgerImage,
    category: "Burgers",
    popular: true,
  },

  {
    id: 11,
    title: "Bacon Burger",
    price: 7.45,
    image: hamburgerImage,
    category: "Burgers",
    popular: false,
  },

  {
    id: 12,
    title: "Dubbele Burger",
    price: 8.95,
    image: hamburgerImage,
    category: "Burgers",
    popular: false,
  },
];