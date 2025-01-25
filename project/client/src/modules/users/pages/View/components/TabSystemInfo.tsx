// source
import { ItemUserDto } from 'src/models/auth/usersDto';

interface DetailsProps {
    user: ItemUserDto;
}

const TabSystemInfo = ({user}: DetailsProps) => {
  return (
    <div
        className="p-3 border rounded bg-light"
        style={{ flex: 1, overflowY: "auto" }}
    >
        <div className="mt-4">
            <strong className="strong-margin-right">
                <i className="bi bi-toggle-on icon-margin-right" /> Active
            </strong>
            {user.isActive ? (
                <i className="bi bi-check-square-fill text-success" /> 
            ) : (
                <i className="bi bi-x-square-fill text-danger" />
            )}
        </div>
    </div>
  )
}

export default TabSystemInfo