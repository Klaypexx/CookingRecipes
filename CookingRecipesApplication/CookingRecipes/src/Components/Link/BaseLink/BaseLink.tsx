import React from 'react';
import { Link } from 'react-router-dom';
import styles from './BaseLink.module.css';
import classNames from 'classnames';
import { LinkProps } from '../../../Types/types';

const BaseLink: React.FC<LinkProps> = ({ primary, navigation, newStyle, linkText, onClick, children }) => {
  const classList = classNames(primary ? undefined : styles.linkSecondary, styles.baseLink);

  const textClassList = classNames(primary ? styles.textPrimary : styles.textSecondary, styles.baseText);

  const styleList = {
    ...newStyle,
    ...(primary ? { backgroundColor: 'rgb(253, 177, 0)' } : undefined),
  };

  return (
    <Link
      to={navigation ? navigation : location.pathname}
      state={{ from: location.pathname }}
      className={classList}
      style={styleList}
      onClick={onClick}
    >
      {children}
      {linkText ? <p className={textClassList}>{linkText}</p> : null}
    </Link>
  );
};

export default BaseLink;
