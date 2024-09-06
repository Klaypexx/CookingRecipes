import { useEffect } from 'react';
import useUserStore from '../../../Stores/useUserStore';
import StatisticListProps from '../../../Types/StatisticListProps';
import BaseStatisticBlock from '../BaseStatisticBlock/BaseStatisticBlock';
import styles from './StatisticList.module.css';

const statisticValuesNames = ['Всего рецептов', 'Всего лайков', 'В избранных'];

const StatisticList: React.FC<StatisticListProps> = ({ values }) => {
  const { likesCount, favouritesCount, setDefaultLikesCount, setDefaultFavouritesCount } = useUserStore();

  useEffect(() => {
    setDefaultLikesCount(values.likesCount);
    setDefaultFavouritesCount(values.favouritesCount);
  }, []);

  const counts = [values.recipesCount, likesCount, favouritesCount];

  return (
    <div className={styles.statisticListFlex}>
      {counts.map((count, index) => (
        <div key={index}>
          <BaseStatisticBlock name={statisticValuesNames[index]} count={count} />
        </div>
      ))}
    </div>
  );
};

export default StatisticList;
