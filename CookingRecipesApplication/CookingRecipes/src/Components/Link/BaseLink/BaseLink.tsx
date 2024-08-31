import React from 'react';
import { Link } from 'react-router-dom';
import styles from './BaseLink.module.css';
import classNames from 'classnames';
import BaseLinkProps from '../../../Types/BaseLinkProps';

const BaseLink: React.FC<BaseLinkProps> = ({ primary, to, className, text, onClick, children }) => {
  const classNameBase = classNames(primary ? styles.linkPrimary : styles.linkSecondary, styles.baseLink);

  const textClassNameBase = classNames(primary ? styles.textPrimary : styles.textSecondary, styles.baseText);

  return (
    <Link
      to={to ? to : location.pathname}
      className={classNames(classNameBase, className)}
      state={{ from: location.pathname }}
      onClick={onClick}
    >
      {children}
      {text ? <p className={textClassNameBase}>{text}</p> : null}
    </Link>
  );
};

export default BaseLink;
