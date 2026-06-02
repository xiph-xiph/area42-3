type Dish = {
    id: string;
    name: string;
    image: string;
}


const dishes : Dish[] = 
    {
        id: "1",
        name: "lasagna",
        image: "lasagna", 
    },   

    {
        id: "2",
        name: "spaghetti",
        image: "spaghetti", 
    },

    {
        id: "3",
        name: "pizza",
        image: "pizza", 
    },


<section>
    
    <h2>Populaire gerechten</h2>

    {dishes.map((dish) => (
        <DishCard 
        key={dish.id}
        name={dish.name}
        image={dish.image}
        />
    ))}
</section>
