using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeFinder.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initDouble2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientEntityRecipeEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientEntityRecipeEntity",
                columns: table => new
                {
                    IngredientsIngredientId = table.Column<int>(type: "integer", nullable: false),
                    RecipesRecipeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientEntityRecipeEntity", x => new { x.IngredientsIngredientId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_IngredientEntityRecipeEntity_Ingredients_IngredientsIngredi~",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientEntityRecipeEntity_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEntityRecipeEntity_RecipesRecipeId",
                table: "IngredientEntityRecipeEntity",
                column: "RecipesRecipeId");
        }
    }
}
