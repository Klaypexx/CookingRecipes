import { useEffect, useState } from 'react';
import StatisticListProps from '../../../Types/StatisticListProps';
import BaseStatisticBlock from '../BaseStatisticBlock/BaseStatisticBlock';
import styles from './StatisticList.module.css';

const statisticValuesNames = ['Всего рецептов', 'Всего лайков', 'В избранных'];

const StatisticList: React.FC<StatisticListProps> = ({ values }) => {
  const [likesCount, setLikesCount] = useState(0);
  const [favouritesCount, setFavouritesCount] = useState(0);

  useEffect(() => {
    setLikesCount(values.likesCount);
    setFavouritesCount(values.favouritesCount);
  }, [values]);

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
