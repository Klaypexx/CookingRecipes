import React from 'react';
import Link from '../BaseLink/BaseLink';
import styles from './LinkBlock.module.css';
import LinkBlockProps from '../../../Types/LinkBlockProps';

const LinkBlock: React.FC<LinkBlockProps> = ({
  linkPrimaryText,
  linkSecondaryText,
  onClickPrimary,
  onClickSecondary,
}) => {
  return (
    <div className={styles.linkBlock}>
      <Link primary text={linkPrimaryText} onClick={onClickPrimary}></Link>
      <Link text={linkSecondaryText} onClick={onClickSecondary}></Link>
    </div>
  );
};

export default LinkBlock;
