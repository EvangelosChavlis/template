// source
import { ItemUserDto } from 'src/models/auth/usersDto'

interface DetailsProps {
    user: ItemUserDto;
}

const TabDetails = ({user}: DetailsProps) => {
  return (
    <div
        className="p-3 border rounded bg-light"
        style={{ flex: 1, overflowY: "auto" }}
    >
        <div className="mt-2">
            <strong className="strong-margin-right">
                <i className="bi bi-person-fill icon-margin-right" />First Name
            </strong>
            <span>{user.firstName}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-person icon-margin-right" />Last Name
            </strong>
            <span>{user.lastName}</span>
        </div>
        <hr />
        <div className="mt-4">
            <strong className="strong-margin-right">
            <i className="bi bi-envelope-fill icon-margin-right" /> Email
            </strong>
            <span>{user.email}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
            <i className="bi bi-envelope  icon-margin-right" />Email Confirmed
            </strong>
            {user.emailConfirmed ? (
            <i className="bi bi-check-square-fill text-success" />
            ) : (
            <i className="bi bi-x-square-fill text-danger" />
            )}
        </div>
        <hr />
        <div className="mt-4">
            <strong className="strong-margin-right">
                <i className="bi bi-person-badge-fill icon-margin-right"/> Username
            </strong>
            <span>{user.userName}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
            <i className="bi bi-key-fill icon-margin-right" />Initial Password
            </strong>
            <span>{user.initialPassword}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-person-fill-lock  icon-margin-right" /> Locked
            </strong>
            {user.lockoutEnabled ? (
            <i className="bi bi-lock-fill text-danger" />
            ) : (
            <i className="bi bi-unlock-fill text-success" />
            )}
        </div>
        <hr />
        <div className="mt-4">
            <strong className="strong-margin-right">
                <i className="bi bi-calendar-event-fill  icon-margin-right" /> Date of Birth
            </strong>
            <span>{user.dateOfBirth}</span>
        </div>
        <div className="mt-5">
            <strong className="strong-margin-right">
                <i className="bi bi-file-earmark-text-fill icon-margin-right" /> Bio
            </strong>
            <span>{user.bio}</span>
        </div>
    </div>
    
  )
}

export default TabDetails