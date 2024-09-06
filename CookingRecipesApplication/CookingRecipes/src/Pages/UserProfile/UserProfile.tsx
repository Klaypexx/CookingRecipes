import { useEffect, useState } from 'react';
import UserForm from '../../Components/Form/UserForm/UserForm';
import RecipesListBlock from '../../Components/Recipe/RecipesList/RecipesList';
import Spinner from '../../Components/Spinner/Spinner';
import StatisticList from '../../Components/Statistic/StatisticList/StatisticList';
import Subheader from '../../Components/Subheader/Subheader';
import RecipeService from '../../Services/RecipeService';
import UserService from '../../Services/UserService';
import useUserStore from '../../Stores/useUserStore';
import UserProfileRecipesValues from '../../Types/UserProfileRecipesValues';
import UserStatisticValues from '../../Types/UserStatisticValues';
import UserValues from '../../Types/UserValues';
import styles from './UserProfile.module.css';

const UserProfile = () => {
  const [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const { userName } = useUserStore();
  const [user, setUser] = useState<UserValues>();
  const [userStatistic, setUserStatistic] = useState<UserStatisticValues>();
  const [recipes, setRecipes] = useState<UserProfileRecipesValues[]>([]);
  const [isLoadButton, setIsLoadButton] = useState(true);
  const [isFirstMount, setIsFirstMount] = useState(true);

  const fetchRecipes = async () => {
    await RecipeService.GetUserRecipes(pageNumber).then((res) => {
      if (res) {
        setIsLoadButton(!res.response.data.isLastRecipes);
        setRecipes((prevValues) => [...prevValues, ...res.response.data.recipes]);
      }
    });
  };

  const fetchUser = async () => {
    await UserService.getUser().then((res) => {
      if (res) {
        setUser(res.response.data);
      }
    });
  };

  const fetchUserStatistic = async () => {
    await UserService.getUserStatistic().then((res) => {
      if (res) {
        setUserStatistic(res.response.data);
      }
    });
  };

  useEffect(() => {
    if (isFirstMount) {
      setIsFirstMount(false);
      return;
    }
    fetchRecipes();
  }, [pageNumber]);

  useEffect(() => {
    if (isFirstMount) {
      setIsFirstMount(false);
      return;
    }
    setRecipes([]);
    fetchRecipes();
  }, [userName]);

  useEffect(() => {
    const fetchData = async () => {
      await Promise.all([fetchRecipes(), fetchUser(), fetchUserStatistic()]);
      setLoading(false);
    };
    fetchData();
  }, []);

  const handleClick = () => {
    setPageNumber((pageNumber) => pageNumber + 1);
  };

  return (
    <div className={styles.userProfile}>
      <section>
        <Subheader backward text="Мой профиль" />
      </section>

      {loading ? (
        <Spinner />
      ) : (
        <>
          <section className={styles.UserFormSection}>
            <UserForm values={user!} />
          </section>

          <section className={styles.StatisticListSection}>
            <StatisticList values={userStatistic!} />
          </section>

          <section className={styles.recipesListSection}>
            <h3 className={styles.recipesListHeader}>Мои рецепты</h3>
            <RecipesListBlock isLoadButton={isLoadButton} handleClick={() => handleClick()} values={recipes} />
          </section>
        </>
      )}
    </div>
  );
};

export default UserProfile;
