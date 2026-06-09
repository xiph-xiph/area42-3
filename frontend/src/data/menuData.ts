export type MenuItemType = {
    name: string;
    description: string;
    price: string;
  };
  
  export type MenuCategoryType = {
    id: string;
    title: string;
    items: MenuItemType[];
  };
  
  export const menuCategories: MenuCategoryType[] = [
    {
      id: "pizza",
      title: "Pizza's",
      items: [
        {
          name: "Pizza Margherita",
          description: "Mozzarella, tomatensaus, oregano",
          price: "€12",
        },
        {
          name: "Pizza Salami",
          description: "Mozzarella, tomatensaus, salami",
          price: "€14",
        },
        {
          name: "Pizza Funghi",
          description: "Mozzarella, tomatensaus, champignons",
          price: "€13",
        },
        {
          name: "Pizza Hawaii",
          description: "Ham, ananas en mozzarella",
          price: "€15",
        },
      ],
    },
  
    {
      id: "pasta",
      title: "Pasta's",
      items: [
        {
          name: "Pasta Bolognese",
          description: "Traditionele Italiaanse vleessaus",
          price: "€14",
        },
        {
          name: "Pasta Carbonara",
          description: "Spek, ei en Parmezaanse kaas",
          price: "€15",
        },
        {
          name: "Pasta Pesto",
          description: "Groene pesto en pijnboompitten",
          price: "€14",
        },
        {
          name: "Pasta Pol",
          description: "Kip, roomsaus en groenten",
          price: "€14",
        },
        {
            name: "Pasta Pollo",
            description: "Kip, roomsaus en groenten",
            price: "€16",
          },

      ],
    },
  
    {
      id: "hoofdgerechten",
      title: "Hoofdgerechten",
      items: [
        {
          name: "Lasagne",
          description: "Ovengebakken lasagne met vleessaus",
          price: "€18",
        },
        {
          name: "Saltimbocca",
          description: "Kalfsvlees met ham en salie",
          price: "€22",
        },
        {
          name: "Risotto Funghi",
          description: "Romige risotto met champignons",
          price: "€19",
        },
      ],
    },
  
    {
      id: "kids",
      title: "Kids",
      items: [
        {
          name: "Kinder Pizza",
          description: "Tomatensaus en mozzarella",
          price: "€8",
        },
        {
          name: "Kinder Pasta",
          description: "Pasta met tomatensaus",
          price: "€8",
        },
        {
          name: "Friet met Snack",
          description: "Friet met frikandel of kroket",
          price: "€7",
        },
      ],
    },
  
    {
      id: "dessert",
      title: "Desserts",
      items: [
        {
          name: "Tiramisu",
          description: "Koffie, mascarpone en cacao",
          price: "€8",
        },
        {
          name: "Panna Cotta",
          description: "Vanille roomdessert",
          price: "€7",
        },
        {
          name: "Gelato",
          description: "Italiaans schepijs",
          price: "€6",
        },
      ],
    },
  ];