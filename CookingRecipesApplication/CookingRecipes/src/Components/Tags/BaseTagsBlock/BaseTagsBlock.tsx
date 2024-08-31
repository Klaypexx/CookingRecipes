import classNames from 'classnames';
import BaseTagsBlockProps from '../../../Types/BaseTagsBlockProps';
import styles from './BaseTagsBlock.module.css';

const BaseTagsBlock: React.FC<BaseTagsBlockProps> = ({ tag, text, className }) => {
  return (
    <>
      <div className={classNames(styles.tagListBox, className)}>
        <div className={styles.iconBox}>
          <img src={tag.icon} alt="icon" className={styles.icon} />
        </div>
        <h3>{tag.header}</h3>
        {text && <p className={styles.tagTextBase}>{tag.text}</p>}
      </div>
    </>
  );
};

export default BaseTagsBlock;
