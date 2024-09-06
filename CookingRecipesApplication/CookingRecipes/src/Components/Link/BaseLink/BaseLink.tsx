import classNames from 'classnames';
import React from 'react';
import { Link } from 'react-router-dom';
import BaseLinkProps from '../../../Types/BaseLinkProps';
import styles from './BaseLink.module.css';

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
